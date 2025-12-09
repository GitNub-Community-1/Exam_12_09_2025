namespace Domain;

public class ProductSupplier
{
    public int ProductId { get; set; }
    public int SupplierId { get; set; }
    
    public decimal PurchasePrice { get; set; }   
    public int LeadTimeDays { get; set; }        
    public DateTime LastSupplyDate { get; set; }
    
    public Product Product { get; set; } = null!;
    public Supplier Supplier { get; set; } = null!;
}