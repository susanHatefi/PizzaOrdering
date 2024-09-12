
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

    
}
