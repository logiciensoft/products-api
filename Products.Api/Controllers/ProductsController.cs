using Microsoft.AspNetCore.Mvc;
using Products.Models;
using Products.Providers.Interfaces;

namespace Products.Api.Controllers;

[Route("api/v1/products")]
[ApiController]
public class ProductsController: ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetFilteredProducts([FromQuery]ProductParam param)
    {
        var filteredProducts = await _productService.GetFilteredProducts(param);

        return Ok(filteredProducts);
    }

}