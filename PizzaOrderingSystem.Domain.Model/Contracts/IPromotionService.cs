namespace PizzaOrderingSystem.Domain.Model.Contracts;

public interface IPromotionService
{
    Task<IEnumerable<Promotion>> GetAllPromotions();
    Task<Promotion?> GetOrderMatchedPromotion(CreateOrder order);
}
