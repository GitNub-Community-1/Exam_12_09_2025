using Microsoft.AspNetCore.Mvc;
using Infastructure.Interface;
using Infastructure.DTOs.SupplierDto;
using Infastructure.Filters;
using Infastructure.Responses;

[ApiController]
[Route("api/suppliers")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _service;

    public SuppliersController(ISupplierService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] SupplierFilter filter)
    {
        var result = await _service.GetSuppliersAsync(filter ?? new SupplierFilter());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetSupplierByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SupplierCreateDto dto)
    {
        var result = await _service.AddSupplierAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SupplierUpdateDto dto)
    {
        var result = await _service.UpdateSupplierAsync(dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteSupplierAsync(id);
        return Ok(result);
    }
}
