using Domain;

namespace Infastructure.Filters;

public class ProductFilter : BaseEntity
{
    public string? Name { get; set; } 
    
    public string? Description { get; set; }

    public int? CurrentStock { get; set; } 

    public decimal? Price { get; set; }
    
    public int? CategoryId { get; set; }

    public int Page { get; set; } = 1;
    public int Size { get; set; } = 20;
}