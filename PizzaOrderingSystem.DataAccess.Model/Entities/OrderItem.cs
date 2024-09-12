using PizzaOrderSystem.DataAccess.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Enumerations;

namespace PizzaOrderSystem.DataAccess.Model.Entities;

public class OrderItem : Entity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? PromotionId { get; set; }
    public short Quantity { get; set; } = 1;
    public decimal TotalPrice { get; set; }
    public DateTimeOffset CreatedDate { get; set;}=DateTimeOffset.Now;

    public virtual Product Product { get; set; }
    public virtual ICollection<Topping> Toppings { get; set; } = new List<Topping>();
    public virtual Promotion? Promotion { get; set; }
    public virtual Order Order { get; set; }

}
