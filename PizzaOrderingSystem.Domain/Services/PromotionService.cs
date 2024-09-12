
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.Domain.Mapping;
using PizzaOrderingSystem.Domain.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Entities;

namespace PizzaOrderingSystem.Domain.Services;

public class PromotionService : IPromotionService
{

    private ILogger<PromotionService> _logger;
    private IRepository<Promotion> _repository;
    public PromotionService(IRepository<Promotion> repository,ILogger<PromotionService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Model.Promotion>> GetAllPromotions()
    {
        try
        {
            var promotions=await _repository.GetAllAsync();
            var domainProductPromotions = promotions?.Select(promotion=>promotion.ToDomain<Promotion,Model.Promotion>()) ;
            
            return domainProductPromotions;
        }
        catch (Exception ex) {
            _logger.LogError(ex,$"{nameof(GetAllPromotions)}");
            throw new Exception("There is a problem to fetch pizza sizes.",ex);
        }
    }

    public async Task<Model.Promotion?> GetOrderMatchedPromotion(Model.CreateOrder order)
    {
        try
        {
            var promotions = await GetAllPromotions();
            foreach (var promotion in promotions)
            {
                if (promotion.Size.ToString() != order.PizzaSize) continue;
                if(promotion.TotalToppings != 0)
                {
                    var orderToppingNo = order.Toppings.Count;
                    if(promotion.TotalToppingsUnit != null)
                    {
                        foreach(var item in promotion.TotalToppingsUnit)
                        {
                            if (order.Toppings.Contains(item.ToppingName))
                            {
                                orderToppingNo = orderToppingNo - 1 + item.Unit;
                            }
                        }
                    }
                    if(orderToppingNo == promotion.TotalToppings)
                    {
                        if (promotion.Quantity > 1 && order.Quantity == promotion.Quantity)
                        {
                            return promotion;
                        }
                        else
                        {
                            return promotion;

                        }

                    }
                    
                }
                else if(promotion.Quantity > 1 && order.Quantity == promotion.Quantity)
                {
                    return promotion;
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(GetOrderMatchedPromotion)}");
            throw new Exception("There is a problem to find matched promotion for the order.", ex);
        }


    }


    
}
