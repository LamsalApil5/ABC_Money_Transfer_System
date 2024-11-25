namespace ABC_Money_Transfer_System.Models
{
    public class ExchangeViewModel
    {
        public decimal? ExchangeRate { get; set; }
          
    }

    public class CurrencyViewModel
    {
        public string? Name { get; set; }
        public string? Iso3 { get; set; }
        public decimal? Buy { get; set; }
        public decimal? Sell { get; set; }

    }

}
