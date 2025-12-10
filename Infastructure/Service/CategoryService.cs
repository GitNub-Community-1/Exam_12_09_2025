using System.Net;
using System.Net.Mime;
using AutoMapper;
using Domain;
using Infastructure.Data;
using Infastructure.DTOs.CategoriDto;
using Infastructure.Filters;
using Infastructure.Interface;
using Infastructure.Responses;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Response<CategoryGetDto>> UpdateCategoryAsync(CategoryUpdateDto updateCategoryDto)
    {
        var category = mapper.Map<Category>(updateCategoryDto);
        context.Categories.Update(category);
        await context.SaveChangesAsync();
        var result = mapper.Map<CategoryGetDto>(category);
        return new Response<CategoryGetDto>(HttpStatusCode.OK,"Updating is succefully!", result);
    }

    public async Task<Response<string>> DeleteCategoryAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        var result = new Response<string>(HttpStatusCode.OK,"Deleting this category is succesfully! : ",$"{category.Id}, {category.Name}, {category.Description}");
        return result;
    }

    public async Task<Response<PagedResponse<List<CategoryGetDto>>>> GetCategoriesAsync(CategoryFilter categoryFilter)
    {
        var query = context.Categories
            .AsQueryable();
        if (categoryFilter.Id.HasValue)
        {
            query = query.Where(x => x.Id == categoryFilter.Id.Value);
        }

        if (!string.IsNullOrEmpty(categoryFilter.Name))
        {
            query = query.Where(x => x.Name.Contains(categoryFilter.Name));
        }

        if (!string.IsNullOrEmpty(categoryFilter.Description))
        {
            query = query.Where(x => x.Description != null && x.Description.Contains(categoryFilter.Description));
        }
        
        var totalRecords = await query.CountAsync();
        
        var page = categoryFilter.Page > 0 ? categoryFilter.Page : 1;
        var size = categoryFilter.Size > 0 ? categoryFilter.Size : 20;

        var categories = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
        
        var result = mapper.Map<List<CategoryGetDto>>(categories);

        
        var pagedResponse = new PagedResponse<List<CategoryGetDto>>
        {
            Data = result,
            Page = page,
            Size = size,
            TotalRecords = totalRecords
        };

        return new Response<PagedResponse<List<CategoryGetDto>>>
        {
            StatusCode  = (int)HttpStatusCode.OK,
            Message = "Your List Categories:",
            Data = pagedResponse
        };
    }

    public async Task<Response<CategoryGetDto>> GetCategoryByIdAsync(int id)
    {
        var category = context.Categories.FirstOrDefaultAsync(n=> n.Id == id);
        await context.SaveChangesAsync();
        var result = new Response<CategoryGetDto>(HttpStatusCode.OK,"Your Category: ", mapper.Map<CategoryGetDto>(category));
        return result;
    }
}