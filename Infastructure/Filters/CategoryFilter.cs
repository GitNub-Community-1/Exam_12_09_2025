using Domain;

namespace Infastructure.Filters;

public class CategoryFilter : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}