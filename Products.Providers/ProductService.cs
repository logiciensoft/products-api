using Microsoft.Extensions.Logging;
using Products.Models;
using Products.Providers.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Products.Providers;

public class ProductService: IProductService
{
    private readonly IAppHelper _appHelper;
    private readonly ILogger<ProductService> _logger;
    private readonly AppSettings _appSettings;

    public ProductService(IAppHelper appHelper, ILogger<ProductService> logger, IOptions<AppSettings> options)
    {
        _appHelper = appHelper;
        _logger = logger;
        _appSettings = options.Value;
    }

    public async Task<ProductResponse> GetFilteredProducts(ProductParam param)
    {
        // log new api request
        
        _logger.LogInformation("API Request Received");
        _logger.LogInformation(JsonConvert.SerializeObject(param, Formatting.Indented));

        // send GET request to products API

        var apiUrl = _appSettings.ProductApiUrl ?? "http://www.mocky.io/v2/5e307edf3200005d00858b49";
        
        _logger.LogInformation($"Fetching products from API, Url={apiUrl}");

        var apiResponse = await _appHelper.FetchProductsFromApi(apiUrl);

        _logger.LogInformation($"API Response:\r\n{apiResponse}\r\n");

        // Deserialize api response
        
        _logger.LogInformation("Deserializing data...");

        var products = JsonConvert.DeserializeObject<ProductApiResponse>(apiResponse);

        if (products == null)
        {
            _logger.LogError("Fail to Deserialize API response.");

            return new ProductResponse()
            {
                Status = 500, Message = "An error occurred while fetching the products"
            };
        }

        _logger.LogInformation("Processing data...");

        // Filter products based on user request

        var filteredProducts = _appHelper.FilterProducts(param.MaxPrice, param.Size, products.Products);

        // Prepare & Log response

        var response = new ProductResponse()
        {
            Status = 200,
            Message = "Successful",
            Products = _appHelper.HighlightProductDescription(filteredProducts, param.Highlight),
            Filter = _appHelper.GetFilter(products.Products)
        };

        _logger.LogInformation("Response Returned");
        _logger.LogInformation(JsonConvert.SerializeObject(response, Formatting.Indented));

        return response;
    }

    
}