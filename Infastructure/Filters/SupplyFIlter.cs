using Domain;

namespace Infastructure.Filters;

public class SupplyFIlter : BaseEntity
{
    public string? CompanyName { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNUmber { get; set; }

    public int Page { get; set; } = 1;
    public int Size { get; set; } = 20;
}