using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Product : BaseEntity
{
    [Required] 
    [MaxLength(200)]
    public string Name { get; set; } = null!;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public int CurrentStock { get; set; } 

    public decimal Price { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    
    public List<Supplier> ProductSuppliers { get; set; } = new();
}