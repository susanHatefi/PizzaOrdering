
namespace PizzaOrderingSystem.Domain.Model;

public record ProductFeature
{
    public IEnumerable<Product> PizzaSize { get; set; }
    public IEnumerable<Topping> VegToppings { get; set; }
    public IEnumerable<Topping> NonVegToppings { get; set; }
    public IEnumerable<Promotion> Promotion { get; set; }

}
