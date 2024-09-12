
using PizzaOrderingSystem.Domain.Model.Enumerations;

namespace PizzaOrderingSystem.Domain.Model;

public record Promotion(string Name): BaseDTO
{
    public string Size { get; set; }
    public Decimal Price { get; set; }
    public short? Discount { get; set; }
    public short TotalToppings { get; set; }
    public List<ToppingUnit>? TotalToppingsUnit { get; set; }
    public int Quantity { get; set; }

}
