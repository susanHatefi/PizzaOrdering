
namespace PizzaOrderingSystem.Domain.Model;

public record Order(IEnumerable<OrderItem> OrderItems, decimal TotalPrice) : BaseDTO
{
}
