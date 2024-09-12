using PizzaOrderSystem.DataAccess.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Enumerations;

namespace PizzaOrderSystem.DataAccess.Model.Entities;

public class Promotion : Entity
{
    public string Name { get; set; }
    public ProductSizeEnum ProductSize { get; set; }
    public decimal Price { get; set; }
    public short? Discount { get; set; }
    public short TotalToppings { get; set; }
    public int Quantity { get; set; }
    public string? Description { get; set; }
    public bool Active { get; set; } = true;
    public virtual IEnumerable<ToppingUnit>? TotalToppingsUnit { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }

}
