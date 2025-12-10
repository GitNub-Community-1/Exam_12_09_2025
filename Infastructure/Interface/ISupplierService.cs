using Infastructure.DTOs.SupplierDto;
using Infastructure.Filters;
using Infastructure.Responses;

namespace Infastructure.Interface;

public interface ISupplierService
{
    public Task<Response<SupplierGetDto>> AddSupplierAsync(SupplierCreateDto createDto);
    public Task<Response<SupplierGetDto>> UpdateSupplierAsync(SupplierUpdateDto updateDto);
    public Task<Response<string>> DeleteSupplierAsync(int id);
    Task<Response<PagedResponse<List<SupplierGetDto>>>> GetSuppliersAsync(SupplierFilter filter);
    public Task<Response<SupplierGetDto>> GetSupplierByIdAsync(int id);
}
