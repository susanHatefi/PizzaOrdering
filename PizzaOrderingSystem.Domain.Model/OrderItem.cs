
namespace PizzaOrderingSystem.Domain.Model;

public record OrderItem (Product PizzaSize,short Quantity=1):BaseDTO
{
    public Promotion Promotion { get; set; }
    public IEnumerable<Topping> Toppings { get; set; }
    public decimal TotalPrice { get; set; }
}
