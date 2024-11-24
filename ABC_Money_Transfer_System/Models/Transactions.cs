namespace ABC_Money_Transfer_System.Models
{
    public class transactions
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public int transferAmount { get; set; }
        public int exchangeRate { get; set; }
        public int payoutAmount  { get; set; }
    }
}
