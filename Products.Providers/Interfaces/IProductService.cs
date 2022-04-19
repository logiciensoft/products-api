using Products.Models;

namespace Products.Providers.Interfaces;

public interface IProductService
{
    Task<ProductResponse> GetFilteredProducts(ProductParam productParam);
}