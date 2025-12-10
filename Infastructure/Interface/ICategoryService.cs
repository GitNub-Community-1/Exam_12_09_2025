using Infastructure.DTOs.CategoriDto;
using Infastructure.Filters;
using Infastructure.Responses;

namespace Infastructure.Interface;

public interface ICategoryService
{
    public Task<Response<CategoryGetDto>> AddCategoryAsync(CategoryCreateDto createCategoryDto);
    public Task<Response<CategoryGetDto>> UpdateCategoryAsync(CategoryUpdateDto updateCategoryDto);
    public Task<Response<string>> DeleteCategoryAsync(int id);
    public Task<Response<List<CategoryGetDto>>> GetCategoriesAsync(CategoryFilter categoryFilter);
    public Task<Response<CategoryGetDto>> GetCategoryById(int id);
}