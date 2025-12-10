using Domain;

namespace Infastructure.Filters;

public class SupplierFilter : BaseEntity
{
    public int? ProductId { get; set; }
    public int? SupplierId { get; set; }
    
    public decimal? PurchasePrice { get; set; }   
    public int? LeadTimeDays { get; set; }        
    public DateTime? LastSupplyDate { get; set; }

    public int Page { get; set; } = 1;
    public int Size { get; set; } = 20;
}