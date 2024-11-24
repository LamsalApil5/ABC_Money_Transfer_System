using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABC_Money_Transfer_System.Models
{
    public class Accounts
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public required string Bank_Name { get; set; }
        [MaxLength(16)]
        public required string AccountNumber { get; set; }        
        public decimal? Balance { get; set; }
        [ForeignKey("Users")] 
        public int? UsersId { get; set; }
        public Users? Users { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
