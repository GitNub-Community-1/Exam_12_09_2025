using Microsoft.AspNetCore.Mvc;
using Infastructure.Interface;
using Infastructure.DTOs.CategoriDto;
using Infastructure.Filters;
using Infastructure.Responses;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CategoryFilter filter)
    {
        var result = await _service.GetCategoriesAsync(filter ?? new CategoryFilter());
        return Ok(result);
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetCategoryByIdAsync(id);
        return Ok(result);
    }

    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
    {
        var result = await _service.AddCategoryAsync(dto);
        return Ok(result);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto dto)
    {
        var result = await _service.UpdateCategoryAsync(dto);
        return Ok(result);
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteCategoryAsync(id);
        return Ok(result);
    }
}