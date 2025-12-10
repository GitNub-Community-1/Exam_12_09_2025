using Domain;

namespace Infastructure.Filters;

public class SupplyFIlter : BaseEntity
{
    public string? CompanyName { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNUmber { get; set; }
}