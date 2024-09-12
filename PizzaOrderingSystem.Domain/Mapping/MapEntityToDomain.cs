

using PizzaOrderingSystem.Domain.Model.Enumerations;
using PizzaOrderSystem.DataAccess.Model.Entities;
using PizzaOrderSystem.DataAccess.Model.Enumerations;

namespace PizzaOrderingSystem.Domain.Mapping;

public static class MapEntityToDomain
{
    public static OutputT? ToDomain<InputT,OutputT>(this InputT entity) where InputT : Entity where OutputT : Model.BaseDTO
    {
        if (entity == null) return null!;
        OutputT? data = entity switch
        {
            Product product => ToPizza(product) as OutputT,
            Topping topping => ToTopping(topping) as OutputT,
            Promotion promotion => ToPromotion(promotion) as OutputT,
            _ => null
        };
        return data;

    }


    public static Model.Product ToPizza(Product pizza)
    {
        var entity = new Model.Product()
        {
            Id = pizza.Id,
            Price = pizza.Price,
            Name=pizza.Size.ToString(),
        };
        return entity;
    }

    
    public static Model.Topping ToTopping(Topping topping)
    {
        var entity = new Model.Topping()
        {
            Id = topping.Id,
            Name = topping.Name,
            Price= topping.Price,
           ToppingType=(ToppingTypeEnum)Enum.Parse(typeof(ToppingCategoryEnum),topping.Category.ToString())

        };
        return entity;
    }

    public static Model.Promotion ToPromotion(Promotion promotion)
    {
        var entity = new Model.Promotion(promotion.ProductSize.ToString())
        {
            Id = promotion.Id,
            Name = promotion.Name,
            Price = promotion.Price,
            Discount = promotion.Discount,
            Quantity = promotion.Quantity,
            TotalToppings = promotion.TotalToppings,
            TotalToppingsUnit = promotion.TotalToppingsUnit?.Select(t => ToToppingUnit(t)).ToList(),
            Size =  promotion.ProductSize.ToString()

        };
        return entity;
    }

    public static Model.ToppingUnit ToToppingUnit(ToppingUnit toppingUnit)
    {
        var entity = new Model.ToppingUnit(toppingUnit.ToppingName, toppingUnit.Unit);
        return entity;
    }


}
