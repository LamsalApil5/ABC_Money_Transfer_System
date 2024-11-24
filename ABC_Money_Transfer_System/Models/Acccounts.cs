using System.ComponentModel.DataAnnotations;

namespace ABC_Money_Transfer_System.Models
{
    public class Acccounts
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public required string Bank_Name { get; set; }
        [MaxLength(16)]
        public required string AccountNumber { get; set; }        
        public double? Balance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
