using Domain;

namespace Infastructure.Filters;

public class SupplierFilter : BaseEntity
{
    public int? ProductId { get; set; }
    public int? SupplierId { get; set; }
    
    public decimal? PurchasePrice { get; set; }   
    public int? LeadTimeDays { get; set; }        
    public DateTime? LastSupplyDate { get; set; }
}