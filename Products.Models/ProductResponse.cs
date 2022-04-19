namespace Products.Models;

public class ProductResponse
{
    public int Status { get; set; } = 200;
    public string Message { get; set; } = default!;
    public ICollection<ProductDto>? Products { get; set; }
    public ProductFilterDto? Filter { get; set; }
}