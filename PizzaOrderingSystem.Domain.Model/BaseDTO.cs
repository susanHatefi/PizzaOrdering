
namespace PizzaOrderingSystem.Domain.Model;

public record BaseDTO()
{
    public Guid Id { get; set; } = Guid.NewGuid();

}
