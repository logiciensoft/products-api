namespace Products.Models;

public class ProductFilterDto
{
    public int MinPrice { get; set; }
    public int MaxPrice { get; set; }
    public string[] Sizes { get; set; } = default!;
    public string[] CommonWords { get; set; } = default!;

}