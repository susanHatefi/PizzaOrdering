namespace PizzaOrderSystem.DataAccess.Model.Entities;

public class Order : Entity
{
    public decimal TotalPrice { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }

}
