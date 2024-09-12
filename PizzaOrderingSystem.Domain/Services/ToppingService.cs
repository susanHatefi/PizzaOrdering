
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.Domain.Mapping;
using PizzaOrderingSystem.Domain.Model.Contracts;
using PizzaOrderingSystem.Domain.Model.Enumerations;
using PizzaOrderSystem.DataAccess.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Entities;
using PizzaOrderSystem.DataAccess.Model.Enumerations;
using System.Linq.Expressions;

namespace PizzaOrderingSystem.Domain.Services;

public class ToppingService : IToppingService
{

    private ILogger<ToppingService> _logger;
    private IRepository<Topping> _repository;
    public ToppingService(IRepository<Topping> repository,ILogger<ToppingService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Model.Topping>> GetAllToppings()
    {
        try
        {
            var toppings = await _repository.GetAllAsync();
            var domainToppings = toppings?.Select(topping => topping.ToDomain<Topping, Model.Topping>());

            return domainToppings;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(GetAllToppings)}");
            throw new Exception("There is a problem to fetch toppings.", ex);
        }
    }

   
    public async Task<(IEnumerable<Model.Topping> VegTopping, IEnumerable<Model.Topping> NonVegTopping)> GetToppingsSeperatedByType()
    {
        try
        {

            var toppings = await _repository.GetAllAsync();
            var vegToppings = toppings?.Where(topping => topping.Category == ToppingCategoryEnum.Veg).Select(topping => topping.ToDomain<Topping, Model.Topping>())?.OrderByDescending(t => t?.Name).ToList();
            var nonVegToppings = toppings?.Where(topping => topping.Category == ToppingCategoryEnum.NonVeg).Select(topping => topping.ToDomain<Topping, Model.Topping>())?.OrderByDescending(t => t?.Name).ToList();
            return (VegTopping:vegToppings,NonVegTopping:nonVegToppings);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(GetToppingsSeperatedByType)}");
            throw new Exception($"There is a problem to fetch toppings seperated By Type.", ex);
        }
    }
}
