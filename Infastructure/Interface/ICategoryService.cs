using Infastructure.DTOs.CategoriDto;
using Infastructure.Filters;
using Infastructure.Responses;

namespace Infastructure.Interface;

public interface ICategoryService
{
    public Task<Response<CategoryGetDto>> AddCategoryAsync(CategoryCreateDto createCategoryDto);
    public Task<Response<CategoryGetDto>> UpdateCategoryAsync(CategoryUpdateDto updateCategoryDto);
    public Task<Response<string>> DeleteCategoryAsync(int id);
    Task<Response<PagedResponse<List<CategoryGetDto>>>> GetCategoriesAsync(CategoryFilter filter);
    public Task<Response<CategoryGetDto>> GetCategoryByIdAsync(int id);
}