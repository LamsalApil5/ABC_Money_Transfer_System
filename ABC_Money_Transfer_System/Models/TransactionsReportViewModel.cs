namespace ABC_Money_Transfer_System.Models
{
    public class TransactionsReportViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<TransactionReportItem> Transactions { get; set; }

    }
    public class TransactionReportItem
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public decimal TransferAmount { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal PayoutAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
