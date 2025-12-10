using System.Net;
using AutoMapper;
using Domain;
using Infastructure.Data;
using Infastructure.DTOs.ProductDto;
using Infastructure.Filters;
using Infastructure.Interface;
using Infastructure.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Service;

public class ProductService(ApplicationDbContext context, IMapper mapper) : IProductService
{
    public async Task<Response<ProductGetDto>> AddProductAsync(ProductCreateDto createDto)
    {
        var product = mapper.Map<Product>(createDto);
        context.Products.Add(product);
        await context.SaveChangesAsync();
        var result = mapper.Map<ProductGetDto>(product);
        return new Response<ProductGetDto>(HttpStatusCode.OK, "Product added successfully!", result);
    }

    public async Task<Response<ProductGetDto>> UpdateProductAsync(ProductUpdateDto updateDto)
    {
        var product = mapper.Map<Product>(updateDto);
        context.Products.Update(product);
        await context.SaveChangesAsync();
        var result = mapper.Map<ProductGetDto>(product);
        return new Response<ProductGetDto>(HttpStatusCode.OK, "Product updated successfully!", result);
    }

    public async Task<Response<string>> DeleteProductAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Product not found!");
        }
        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "Product deleted successfully!", $"{product.Id}, {product.Name}, {product.Price}");
    }

    public async Task<Response<PagedResponse<List<ProductGetDto>>>> GetProductsAsync(ProductFilter filter)
    {
        var query = context.Products.AsQueryable();
        
        if (filter.Id.HasValue)
        {
            query = query.Where(x => x.Id == filter.Id.Value);
        }

        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.Contains(filter.Name));
        }

        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(x => x.Description != null && x.Description.Contains(filter.Description));
        }

        if (filter.CurrentStock.HasValue)
        {
            query = query.Where(x => x.CurrentStock == filter.CurrentStock.Value);
        }

        if (filter.Price.HasValue)
        {
            query = query.Where(x => x.Price == filter.Price.Value);
        }

        if (filter.CategoryId.HasValue)
        {
            query = query.Where(x => x.CategoryId == filter.CategoryId.Value);
        }
        
        var totalRecords = await query.CountAsync();
        
        var page = filter.Page > 0 ? filter.Page : 1;
        var size = filter.Size > 0 ? filter.Size : 20;

        var products = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
        
        var result = mapper.Map<List<ProductGetDto>>(products);

        var pagedResponse = new PagedResponse<List<ProductGetDto>>
        {
            Data = result,
            Page = page,
            Size = size,
            TotalRecords = totalRecords
        };

        return new Response<PagedResponse<List<ProductGetDto>>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Your list of products:",
            Data = pagedResponse
        };
    }

    public async Task<Response<ProductGetDto>> GetProductByIdAsync(int id)
    {
        var product = await context.Products.FirstOrDefaultAsync(n => n.Id == id);
        if (product == null)
        {
            return new Response<ProductGetDto>(HttpStatusCode.NotFound, "Product not found!");
        }
        var result = mapper.Map<ProductGetDto>(product);
        return new Response<ProductGetDto>(HttpStatusCode.OK, "Your product:", result);
    }
}
