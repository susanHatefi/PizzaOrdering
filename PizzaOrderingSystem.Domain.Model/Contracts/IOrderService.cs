using PizzaOrderingSystem.Domain.Model.Enumerations;
using System.Linq.Expressions;

namespace PizzaOrderingSystem.Domain.Model.Contracts;

public interface IOrderService
{
    Task SaveOrders(IEnumerable<Model.CreateOrder> orders);
}
