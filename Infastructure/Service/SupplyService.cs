using System.Net;
using AutoMapper;
using Domain;
using Infastructure.Data;
using Infastructure.DTOs.SupplyDto;
using Infastructure.Filters;
using Infastructure.Interface;
using Infastructure.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Service;

public class SupplyService(ApplicationDbContext context, IMapper mapper) : ISupplyService
{
    public async Task<Response<SupplyGetDto>> AddSupplyAsync(SupplyCreateDto createDto)
    {
        var supply = mapper.Map<Supply>(createDto);
        context.Supply.Add(supply);
        await context.SaveChangesAsync();
        var result = mapper.Map<SupplyGetDto>(supply);
        return new Response<SupplyGetDto>(HttpStatusCode.OK, "Supply added successfully!", result);
    }

    public async Task<Response<SupplyGetDto>> UpdateSupplyAsync(SupplyUpdateDto updateDto)
    {
        var supply = mapper.Map<Supply>(updateDto);
        context.Supply.Update(supply);
        await context.SaveChangesAsync();
        var result = mapper.Map<SupplyGetDto>(supply);
        return new Response<SupplyGetDto>(HttpStatusCode.OK, "Supply updated successfully!", result);
    }

    public async Task<Response<string>> DeleteSupplyAsync(int id)
    {
        var supply = await context.Supply.FindAsync(id);
        if (supply == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Supply not found!");
        }
        context.Supply.Remove(supply);
        await context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "Supply deleted successfully!", $"{supply.Id}, {supply.Count}, {supply.SupplyDate}");
    }

    public async Task<Response<PagedResponse<List<SupplyGetDto>>>> GetSuppliesAsync(SupplyFIlter filter)
    {
        var query = context.Supply.AsQueryable();
        
        if (filter.Id.HasValue)
        {
            query = query.Where(x => x.Id == filter.Id.Value);
        }
        
        var totalRecords = await query.CountAsync();
        
        var page = filter.Page > 0 ? filter.Page : 1;
        var size = filter.Size > 0 ? filter.Size : 20;

        var supplies = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
        
        var result = mapper.Map<List<SupplyGetDto>>(supplies);

        var pagedResponse = new PagedResponse<List<SupplyGetDto>>
        {
            Data = result,
            Page = page,
            Size = size,
            TotalRecords = totalRecords
        };

        return new Response<PagedResponse<List<SupplyGetDto>>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Your list of supplies:",
            Data = pagedResponse
        };
    }

    public async Task<Response<SupplyGetDto>> GetSupplyByIdAsync(int id)
    {
        var supply = await context.Supply.FirstOrDefaultAsync(n => n.Id == id);
        if (supply == null)
        {
            return new Response<SupplyGetDto>(HttpStatusCode.NotFound, "Supply not found!");
        }
        var result = mapper.Map<SupplyGetDto>(supply);
        return new Response<SupplyGetDto>(HttpStatusCode.OK, "Your supply:", result);
    }
}
