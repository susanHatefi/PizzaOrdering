using PizzaOrderingSystem.Domain.Model.Enumerations;
using System.Linq.Expressions;

namespace PizzaOrderingSystem.Domain.Model.Contracts;

public interface IToppingService
{
    Task<IEnumerable<Topping>> GetAllToppings();
    Task<(IEnumerable<Topping> VegTopping, IEnumerable<Topping> NonVegTopping)> GetToppingsSeperatedByType();

}
