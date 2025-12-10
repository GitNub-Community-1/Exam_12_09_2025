using Microsoft.AspNetCore.Mvc;
using Infastructure.Interface;
using Infastructure.DTOs.ProductDto;
using Infastructure.Filters;
using Infastructure.Responses;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ProductFilter filter)
    {
        var result = await _service.GetProductsAsync(filter ?? new ProductFilter());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetProductByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
    {
        var result = await _service.AddProductAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto dto)
    {
        var result = await _service.UpdateProductAsync(dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteProductAsync(id);
        return Ok(result);
    }
}
