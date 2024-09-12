
namespace PizzaOrderingSystem.Domain.Model;

public record Product:BaseDTO
{
    public string Name { get; set; }
    public Decimal Price { get; set; }
}
