using Microsoft.AspNetCore.Mvc;
using Infastructure.Interface;
using Infastructure.DTOs.SupplyDto;
using Infastructure.Filters;
using Infastructure.Responses;

[ApiController]
[Route("api/supplies")]
public class SuppliesController : ControllerBase
{
    private readonly ISupplyService _service;

    public SuppliesController(ISupplyService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] SupplyFIlter filter)
    {
        var result = await _service.GetSuppliesAsync(filter ?? new SupplyFIlter());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetSupplyByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SupplyCreateDto dto)
    {
        var result = await _service.AddSupplyAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SupplyUpdateDto dto)
    {
        var result = await _service.UpdateSupplyAsync(dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteSupplyAsync(id);
        return Ok(result);
    }
}
