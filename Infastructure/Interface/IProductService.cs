using Infastructure.DTOs.ProductDto;
using Infastructure.Filters;
using Infastructure.Responses;

namespace Infastructure.Interface;

public interface IProductService
{
    public Task<Response<ProductGetDto>> AddProductAsync(ProductCreateDto createDto);
    public Task<Response<ProductGetDto>> UpdateProductAsync(ProductUpdateDto updateDto);
    public Task<Response<string>> DeleteProductAsync(int id);
    Task<Response<PagedResponse<List<ProductGetDto>>>> GetProductsAsync(ProductFilter filter);
    public Task<Response<ProductGetDto>> GetProductByIdAsync(int id);
}
