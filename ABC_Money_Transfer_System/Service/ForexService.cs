using Newtonsoft.Json;
using ABC_Money_Transfer_System.Models;

public class ForexService
{
    private readonly HttpClient _httpClient;

    // Constructor to initialize HttpClient
    public ForexService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ForexApiResponse> GetForexRatesAsync(int page = 1, int perPage = 5, DateTime? date = null)
    {
        try
        {
            var today = date ?? DateTime.Today;

            var fromDate = today.ToString("yyyy-MM-dd");
            var toDate = today.ToString("yyyy-MM-dd");

            // Construct the API URL with dynamic date
            var url = $"https://www.nrb.org.np/api/forex/v1/rates?page={page}&per_page={perPage}&from={fromDate}&to={toDate}";

            // Make the GET request
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var forexApiResponse = JsonConvert.DeserializeObject<ForexApiResponse>(responseData);

            return forexApiResponse;
        }
        catch (Exception ex)
        {
            return null;           
        }
    }
    public async Task<List<CurrencyViewModel>> GetExchangeCurrency()
    {
        var getForexRates = await GetForexRatesAsync();
        var rates = getForexRates.Data.Payload.SelectMany(d => d.Rates).ToList();

        var result = rates.Select(rate => new CurrencyViewModel
        {
            Name = rate.Currency.Name,
            Iso3 = rate.Currency.Iso3,
            Buy = decimal.TryParse(rate.Buy, out var buyValue) ? (decimal?)buyValue : null,
            Sell = decimal.TryParse(rate.Sell, out var sellValue) ? (decimal?)sellValue : null
        }).ToList();

        return result;
    }


    public async Task<ExchangeViewModel> CalculateAmount(string? iso3, decimal? balance)
    {
        // Check if balance is null
        if (balance == null)
        {
            throw new ArgumentNullException(nameof(balance), "Balance cannot be null.");
        }

        // Fetch forex rates for the given date (use today's date as default)
        var forexRates = await GetForexRatesAsync(date: DateTime.Today);

        // Find the exchange rate for the specific currency (ISO3 code)
        var rate = forexRates.Data.Payload
                              .SelectMany(d => d.Rates)
                              .FirstOrDefault(r => r.Currency.Iso3 == iso3);

        if (rate == null)
        {
            throw new Exception($"Exchange rate for currency {iso3} not found.");
        }

        // Safely convert the buy rate string to a decimal
        if (!decimal.TryParse(rate.Buy, out decimal buyRate))
        {
            throw new Exception($"Invalid exchange rate format for {iso3}.");
        }

        // Calculate the balance in the target currency
        var convertedAmount = balance.Value * buyRate;

        // Return the result in an ExchangeViewModel
        return new ExchangeViewModel
        {
            ExchangeRate = convertedAmount
        };
    }




}
