using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Country
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;   
    public string? Currency { get; set; }
    public string? CurrencyCode { get; set; }
}
