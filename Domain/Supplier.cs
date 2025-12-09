using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Supplier : BaseEntity
{
    [Required] 
    [MaxLength(150)]
    public string CompanyName { get; set; }
    [EmailAddress]
    [MaxLength(70)]
    public string EmailAddress { get; set; }
    [Phone]
    public string PhoneNUmber { get; set; }
    public List<Supplier> ProductSuppliers { get; set; } = new();

}