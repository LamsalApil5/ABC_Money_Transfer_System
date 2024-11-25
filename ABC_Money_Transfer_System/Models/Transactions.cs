using System.ComponentModel.DataAnnotations;

namespace ABC_Money_Transfer_System.Models
{
    public class Transactions
    {
        [Key]
        public int Id { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public decimal transferAmount { get; set; }
        public decimal exchangeRate { get; set; }
        public decimal payoutAmount  { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
