using System.Net;
using System.Net.Mime;
using AutoMapper;
using Domain;
using Infastructure.Data;
using Infastructure.DTOs.CategoriDto;
using Infastructure.Filters;
using Infastructure.Interface;
using Infastructure.Responses;

namespace Infastructure.Service;

public class CategoryService(ApplicationDbContext context, IMapper mapper) : ICategoryService
{
    public async Task<Response<CategoryGetDto>> AddCategoryAsync(CategoryCreateDto createCategoryDto)
    {
        var category = mapper.Map<Category>(createCategoryDto);
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        var result = mapper.Map<CategoryGetDto>(category);
        return new Response<CategoryGetDto>(HttpStatusCode.OK,"Added succefully!", result);
    }

    public Task<Response<CategoryGetDto>> UpdateCategoryAsync(CategoryUpdateDto updateCategoryDto)
    {
        
    }

    public Task<Response<string>> DeleteCategoryAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Response<List<CategoryGetDto>>> GetCategoriesAsync(CategoryFilter categoryFilter)
    {
        throw new NotImplementedException();
    }

    public Task<Response<CategoryGetDto>> GetCategoryById(int id)
    {
        throw new NotImplementedException();
    }
}