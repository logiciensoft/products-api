using System.Net.Http.Headers;
using Products.Models;
using Products.Providers.Interfaces;

namespace Products.Providers;

public class AppHelper: IAppHelper
{
    private static readonly HttpClient Client = new();

    public AppHelper()
    {
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> FetchProductsFromApi(string url)
    {
        var response = await Client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }

    public List<ProductDto> HighlightProductDescription(List<ProductDto> products, string? highlight)
    {
        var returnProducts = products;

        // check if highlight data has been submitted
        if (!string.IsNullOrEmpty(highlight) && products.Any())
        {
            // transform highlight values into a list for iteration
            var highlightList = highlight.Trim().Split(",").ToList();

            //find and replace highlight values in each product
            returnProducts.ForEach(productDto =>
            {
                if (!string.IsNullOrEmpty(productDto.Description) && highlightList.Any())
                {
                    foreach (var highlightValue in highlightList)
                    {
                        var index = productDto.Description.IndexOf(highlightValue.Trim(), StringComparison.Ordinal);
                        if (index < 0)
                            continue;

                        productDto.Description = productDto.Description
                            .Replace(highlightValue.Trim(), $"<em>{highlightValue}</em>");
                    }
                }
            });
        }

        return returnProducts;
    }

    public List<ProductDto> FilterProducts(double? maxPrice, string? size, List<ProductDto> products)
    {
        var filteredProducts = products;

        if (maxPrice is > 0)
            filteredProducts = filteredProducts.Where(p => p.Price <= maxPrice.Value).ToList();

        if (!string.IsNullOrWhiteSpace(size))
            filteredProducts = filteredProducts.Where(p => p.Sizes.Contains(size)).ToList();

        return filteredProducts;

    }

    public ProductFilterDto GetFilter(List<ProductDto> products)
    {
        // Get min & max price
        var minPrice = products.Min(p => p.Price);
        var maxPrice = products.Max(p => p.Price);

        // get all sizes of products
        var sizeList = products.SelectMany(p => p.Sizes)
            .Select(p => p)
            .Distinct()
            .ToArray();

        //combine products descriptions and count words to get the common words
        var allProductDescription = string.Join(" ", products.Select(p => p.Description).ToList());

        return new ProductFilterDto()
        {
            MaxPrice = maxPrice,
            MinPrice = minPrice,
            Sizes = sizeList,
            CommonWords = GetMostCommonWords(allProductDescription, 5, 10)
        };
    }

    public string[] GetMostCommonWords(string text, int skip, int take)
    {
        Dictionary<string, int> stats = new Dictionary<string, int>();
        char[] chars = { ' ', '.', ',', ';', ':', '?', '\n', '\r' };

        // split words
        string[] words = text.Split(chars);
        int minWordLength = 2;// to count words having more than 2  characters

        // iterate over the word collection to count occurrences
        foreach (string word in words)
        {
            string w = word.Trim().ToLower();
            if (w.Length > minWordLength)
            {
                if (!stats.ContainsKey(w))
                {
                    // add new word to collection
                    stats.Add(w, 1);
                }
                else
                {
                    // update word occurrence count
                    stats[w] += 1;
                }
            }
        }

        return stats.OrderByDescending(x => x.Value)
            .Skip(skip)
            .Take(take)
            .Select(p => p.Key)
            .ToArray();
    }
}