namespace Products.Models;

public class ProductApiResponse
{
    public List<ProductDto> Products { get; set; } = new List<ProductDto>();
}