using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Country
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(2)]
    public string Iso { get; set; } = string.Empty;

    [Required, MaxLength(80)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(80)]
    public string NiceName { get; set; } = string.Empty;

    [MaxLength(3)]
    public string? Iso3 { get; set; }

    public short? NumCode { get; set; }

    [Required]
    public int PhoneCode { get; set; }
}
