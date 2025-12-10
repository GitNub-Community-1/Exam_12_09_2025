using Infastructure.DTOs.SupplyDto;
using Infastructure.Filters;
using Infastructure.Responses;

namespace Infastructure.Interface;

public interface ISupplyService
{
    public Task<Response<SupplyGetDto>> AddSupplyAsync(SupplyCreateDto createDto);
    public Task<Response<SupplyGetDto>> UpdateSupplyAsync(SupplyUpdateDto updateDto);
    public Task<Response<string>> DeleteSupplyAsync(int id);
    Task<Response<PagedResponse<List<SupplyGetDto>>>> GetSuppliesAsync(SupplyFIlter filter);
    public Task<Response<SupplyGetDto>> GetSupplyByIdAsync(int id);
}
