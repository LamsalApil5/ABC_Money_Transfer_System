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
            ViewBag.Users = new SelectList(_context.Users, "Id", "FirstName", "Country");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(int senderId, int receiverId, int transferAmount)
        {
            if (transferAmount <= 0)
            {
                return BadRequest("Transfer amount must be greater than zero.");
            }

            // Fetch today's exchange rates
            var forexRates = await _forexService.GetForexRatesAsync();

            // Get the exchange rate for MYR to NPR
            var myrRate = forexRates.Data.Payload
                                     .SelectMany(d => d.Rates)
                                     .FirstOrDefault(r => r.Currency.Iso3 == "MYR");

            var nprRate = forexRates.Data.Payload
                                     .SelectMany(d => d.Rates)
                                     .FirstOrDefault(r => r.Currency.Iso3 == "NPR");

            if (myrRate == null || nprRate == null)
            {
                return BadRequest("Exchange rate for MYR or NPR not found.");
            }

            if (!decimal.TryParse(myrRate.Buy, out decimal myrBuyRate) || 
                !decimal.TryParse(nprRate.Buy, out decimal nprBuyRate))
            {
                return BadRequest("Invalid exchange rate format.");
            }

            // Calculate the exchange rate (MYR to NPR)
            decimal exchangeRate = myrBuyRate / nprBuyRate;

            // Calculate payout amount
            decimal payoutAmount = transferAmount * exchangeRate;

            // Create the transaction
            var transaction = new transactions
            {
                senderId = senderId,
                receiverId = receiverId,
                transferAmount = transferAmount,
                exchangeRate = (int)Math.Round(exchangeRate),
                payoutAmount = (int)Math.Round(payoutAmount)
            };


            return Ok(transaction);
        }
    }
}
