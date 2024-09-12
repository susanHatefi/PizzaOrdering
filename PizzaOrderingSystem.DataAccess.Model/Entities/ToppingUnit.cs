namespace PizzaOrderSystem.DataAccess.Model.Entities;

public class ToppingUnit:Entity
{
    public string ToppingName { get; set; }
    public short Unit { get; set; }
    public Guid PromotionId { get; set; }
    public virtual Promotion Promotion { get; set; }
}