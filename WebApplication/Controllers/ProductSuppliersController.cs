using Microsoft.AspNetCore.Mvc;
using Infastructure.Interface;
using Infastructure.Filters;
using Infastructure.Responses;

[ApiController]
[Route("api/product-suppliers")]
public class ProductSuppliersController : ControllerBase
{
    private readonly IProductSupplierService _service;

    public ProductSuppliersController(IProductSupplierService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] SupplierFilter filter)
    {
        var result = await _service.GetProductSuppliersAsync(filter ?? new SupplierFilter());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetProductSupplierByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromQuery] int productId, [FromQuery] int supplierId)
    {
        var result = await _service.AddProductSupplierAsync(productId, supplierId);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id)
    {
        var result = await _service.UpdateProductSupplierAsync(id);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteProductSupplierAsync(id);
        return Ok(result);
    }
}
