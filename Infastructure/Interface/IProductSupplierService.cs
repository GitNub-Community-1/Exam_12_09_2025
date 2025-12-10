using Infastructure.Responses;
using Infastructure.DTOs.ProductDto;
using Infastructure.DTOs.SupplierDto;
using Infastructure.Filters;

namespace Infastructure.Interface;

public interface IProductSupplierService
{
    public Task<Response<string>> AddProductSupplierAsync(int productId, int supplierId);
    public Task<Response<string>> UpdateProductSupplierAsync(int id);
    public Task<Response<string>> DeleteProductSupplierAsync(int id);
    Task<Response<PagedResponse<List<object>>>> GetProductSuppliersAsync(SupplierFilter filter);
    public Task<Response<object>> GetProductSupplierByIdAsync(int id);
}
