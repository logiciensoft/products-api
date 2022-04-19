using Products.Models;

namespace Products.Providers.Interfaces;

public interface IAppHelper
{
    Task<string> FetchProductsFromApi(string url);
    List<ProductDto> HighlightProductDescription(List<ProductDto> products, string? highlight);
    List<ProductDto> FilterProducts(double? maxPrice, string? size, List<ProductDto> products);
    ProductFilterDto GetFilter(List<ProductDto> products);
    string[] GetMostCommonWords(string text, int skip, int take);
}