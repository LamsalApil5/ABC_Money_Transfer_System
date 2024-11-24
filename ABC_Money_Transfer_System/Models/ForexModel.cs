namespace ABC_Money_Transfer_System.Models
{
    public class ForexApiResponse
    {
        public Status Status { get; set; }
        public Errors Errors { get; set; }
        public Params Params { get; set; }
        public Data Data { get; set; }
        public Pagination Pagination { get; set; }
    }

    public class Status
    {
        public int Code { get; set; }
    }

    public class Errors
    {
        public object Validation { get; set; }
    }

    public class Params
    {
        public string Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int PerPage { get; set; }
        public int Page { get; set; }
    }

    public class Data
    {
        public List<ForexData> Payload { get; set; }
    }

    public class ForexData
    {
        public string Date { get; set; }
        public string PublishedOn { get; set; }
        public string ModifiedOn { get; set; }
        public List<CurrencyRate> Rates { get; set; }
    }

    public class CurrencyRate
    {
        public Currency Currency { get; set; }
        public string Buy { get; set; }
        public string Sell { get; set; }
    }

    public class Currency
    {
        public string Iso3 { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
    }

    public class Pagination
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
    }


}
