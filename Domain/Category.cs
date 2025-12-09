using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Category : BaseEntity
{
    [Required] 
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }
    public List<Product> Products { get; set; } = new();
}
