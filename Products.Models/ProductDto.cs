namespace Products.Models;

public class ProductDto
{
    public string Title { get; set; } = default!;
    public int Price { get; set; }
    public ICollection<string> Sizes { get; set; } = new List<string>();
    public string Description { get; set; } = default!;
}