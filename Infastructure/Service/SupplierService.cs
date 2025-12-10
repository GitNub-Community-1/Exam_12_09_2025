using System.Net;
using AutoMapper;
using Domain;
using Infastructure.Data;
using Infastructure.DTOs.SupplierDto;
using Infastructure.Filters;
using Infastructure.Interface;
using Infastructure.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Service;

public class SupplierService(ApplicationDbContext context, IMapper mapper) : ISupplierService
{
    public async Task<Response<SupplierGetDto>> AddSupplierAsync(SupplierCreateDto createDto)
    {
        var supplier = mapper.Map<Supplier>(createDto);
        context.Suppliers.Add(supplier);
        await context.SaveChangesAsync();
        var result = mapper.Map<SupplierGetDto>(supplier);
        return new Response<SupplierGetDto>(HttpStatusCode.OK, "Supplier added successfully!", result);
    }

    public async Task<Response<SupplierGetDto>> UpdateSupplierAsync(SupplierUpdateDto updateDto)
    {
        var supplier = mapper.Map<Supplier>(updateDto);
        context.Suppliers.Update(supplier);
        await context.SaveChangesAsync();
        var result = mapper.Map<SupplierGetDto>(supplier);
        return new Response<SupplierGetDto>(HttpStatusCode.OK, "Supplier updated successfully!", result);
    }

    public async Task<Response<string>> DeleteSupplierAsync(int id)
    {
        var supplier = await context.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Supplier not found!");
        }
        context.Suppliers.Remove(supplier);
        await context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "Supplier deleted successfully!", $"{supplier.Id}, {supplier.CompanyName}, {supplier.EmailAddress}");
    }

    public async Task<Response<PagedResponse<List<SupplierGetDto>>>> GetSuppliersAsync(SupplierFilter filter)
    {
        var query = context.Suppliers.AsQueryable();
        
        if (filter.Id.HasValue)
        {
            query = query.Where(x => x.Id == filter.Id.Value);
        }
        
        var totalRecords = await query.CountAsync();
        
        var page = filter.Page > 0 ? filter.Page : 1;
        var size = filter.Size > 0 ? filter.Size : 20;

        var suppliers = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
        
        var result = mapper.Map<List<SupplierGetDto>>(suppliers);

        var pagedResponse = new PagedResponse<List<SupplierGetDto>>
        {
            Data = result,
            Page = page,
            Size = size,
            TotalRecords = totalRecords
        };

        return new Response<PagedResponse<List<SupplierGetDto>>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Your list of suppliers:",
            Data = pagedResponse
        };
    }

    public async Task<Response<SupplierGetDto>> GetSupplierByIdAsync(int id)
    {
        var supplier = await context.Suppliers.FirstOrDefaultAsync(n => n.Id == id);
        if (supplier == null)
        {
            return new Response<SupplierGetDto>(HttpStatusCode.NotFound, "Supplier not found!");
        }
        var result = mapper.Map<SupplierGetDto>(supplier);
        return new Response<SupplierGetDto>(HttpStatusCode.OK, "Your supplier:", result);
    }
}
