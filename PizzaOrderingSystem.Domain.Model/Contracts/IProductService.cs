namespace PizzaOrderingSystem.Domain.Model.Contracts;
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProducts();
}
