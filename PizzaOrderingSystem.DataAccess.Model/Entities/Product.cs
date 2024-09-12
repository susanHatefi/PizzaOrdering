using PizzaOrderSystem.DataAccess.Model.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaOrderSystem.DataAccess.Model.Entities;

public class Product : Entity
{

    public ProductSizeEnum Size { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }

}
