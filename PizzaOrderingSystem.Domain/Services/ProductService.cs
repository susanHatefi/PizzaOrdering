
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.Domain.Mapping;
using PizzaOrderingSystem.Domain.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Entities;

namespace PizzaOrderingSystem.Domain.Services;

public class ProductService : IProductService
{

    private ILogger<ProductService> _logger;
    private IRepository<Product> _repository;
    public ProductService(IRepository<Product> repository,ILogger<ProductService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Model.Product>> GetAllProducts()
    {
        try
        {
            var products=await _repository.GetAllAsync();
            var domainProducts = products?.Select(product => product.ToDomain<Product, Model.Product>()) ;
            
            return domainProducts;
        }
        catch (Exception ex) {
            _logger.LogError(ex,$"{nameof(GetAllProducts)}");
            throw new Exception("There is a problem to fetch pizza sizes.",ex);
        }
    }

}
