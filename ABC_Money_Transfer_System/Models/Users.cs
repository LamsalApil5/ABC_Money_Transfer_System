using System.ComponentModel.DataAnnotations;

namespace ABC_Money_Transfer_System.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public required string FirstName { get; set; }

        [MaxLength(100)]
        public string? MiddleName { get; set; }

        [MaxLength(100)]
        public required string LastName { get; set; }

        [MaxLength(255)]
        public required string Address { get; set; }

        [MaxLength(255)]
        public required string Country { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
