
namespace PizzaOrderingSystem.Domain.Model;

public record CreateOrder(string PizzaSize, HashSet<string> Toppings, short Quantity = 1)
{

}