
using PizzaOrderingSystem.Domain.Model.Enumerations;

namespace PizzaOrderingSystem.Domain.Model;

public record Topping: BaseDTO
{
    public string Name { get; set; }
    public Decimal Price { get; set; }

    public ToppingTypeEnum ToppingType { get; set; }
}
