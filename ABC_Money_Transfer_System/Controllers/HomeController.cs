using System.Diagnostics;
using ABC_Money_Transfer_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABC_Money_Transfer_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ForexService service;
        public HomeController(ILogger<HomeController> logger, ForexService service)
        {
            _logger = logger;
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var forexResponse = await service.GetForexRatesAsync();
            if (forexResponse?.Data?.Payload == null || !forexResponse.Data.Payload.Any())
            {
                return View(new List<CurrencyRate>());
            }

            var rates = forexResponse.Data.Payload
                .SelectMany(forexData => forexData.Rates.Select(rate => new CurrencyRate
                {
                    Currency = rate.Currency,
                    Buy = rate.Buy ?? "N/A",
                    Sell = rate.Sell ?? "N/A"
                }))
                .ToList();

            return View(rates);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
