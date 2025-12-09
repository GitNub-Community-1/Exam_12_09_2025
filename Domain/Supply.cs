namespace Domain;

public class Supply : BaseEntity
{
    public int Count { get; set; }
    public DateTime SupplyDate { get; set; }
    
    
    public int ProductId { get; set; }
    public Product Product { get; set; }
    
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
}