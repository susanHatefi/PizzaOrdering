using PizzaOrderSystem.DataAccess.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Enumerations;

namespace PizzaOrderSystem.DataAccess.Model.Entities;

public class Topping : Entity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ToppingCategoryEnum Category { get; set; }

    public virtual ICollection<OrderItem>  OrderItems { get; set; }=new List<OrderItem>();
}
