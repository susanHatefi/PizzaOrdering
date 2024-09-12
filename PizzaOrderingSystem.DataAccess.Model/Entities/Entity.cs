using PizzaOrderSystem.DataAccess.Model.Contracts;

namespace PizzaOrderSystem.DataAccess.Model.Entities;

public class Entity : IEntity
{
    public Guid Id { get; init; }
}
