using ABC_Money_Transfer_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ABC_Money_Transfer_System.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ForexService _forexService;
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context, ForexService forexService)
        {
            _forexService = forexService;
            _context = context;
        }
        public IActionResult Create()
        {
            var users = _context.Users
                                .Select(u => new { u.Id, u.FirstName, u.Country })
                                .ToList();

            var countries = _context.Countries
                                    .Select(c => new { c.Id, c.CurrencyCode, c.Name })
                                    .ToList();

            var userSelectList = users.Select(user => new SelectListItem
            {
                Value = user.Id.ToString(),
                Text = $"{user.FirstName} ({countries.FirstOrDefault(c => c.Name == user.Country)?.CurrencyCode ?? "Unknown"})"
            }).ToList();

            var exchangeRates = _forexService.GetExchangeCurrency().Result
                                             .ToDictionary(r => r.Iso3, r => r.Buy);

            ViewBag.Users = userSelectList;
            ViewBag.ExchangeRates = exchangeRates;

            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Transactions transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.payoutAmount = transaction.transferAmount * transaction.exchangeRate;

                // Save transaction to the database
                _context.Transactions.Add(transaction);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }



        public IActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            var transactionsQuery = _context.Transactions.AsQueryable();

            if (startDate.HasValue)
            {
                transactionsQuery = transactionsQuery.Where(t => t.CreatedAt >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                transactionsQuery = transactionsQuery.Where(t => t.CreatedAt <= endDate.Value);
            }

            var transactions = transactionsQuery.ToList();

            // Create the model to pass to the view
            var model = new TransactionsReportViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                Transactions = transactions.Select(t => new TransactionReportItem
                {
                    Id = t.Id,
                    SenderName = _context.Users.Where(u => u.Id == t.senderId).Select(u => u.FirstName + " " + u.MiddleName + " " + u.LastName).FirstOrDefault() ?? "",
                    ReceiverName = _context.Users.Where(u => u.Id == t.receiverId).Select(u => u.FirstName + " " + u.MiddleName + " " + u.LastName).FirstOrDefault() ?? "",
                    TransferAmount = t.transferAmount,
                    ExchangeRate = t.exchangeRate,
                    PayoutAmount = t.payoutAmount,
                    CreatedAt = t.CreatedAt
                }).ToList()
            };

            return View(model);
        }


    }
}
