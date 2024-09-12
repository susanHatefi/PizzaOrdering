
using PizzaOrderSystem.DataAccess.Model.Entities;
using PizzaOrderSystem.DataAccess.Model.Enumerations;
using System.Linq.Expressions;

namespace PizzaOrderingSystem.Domain.Mapping;

public static class MapDomainToEntity
{
    public static OutputT? ToEntity<InputT, OutputT>(this InputT entity) where InputT : Model.BaseDTO where OutputT : Entity
    {
        if (entity == null) return null!;
        OutputT? data = entity switch
        {
            Model.OrderItem order => ToOrder(order) as OutputT,
            Model.Product pizza => ToProduct(pizza) as OutputT,
            Model.Topping topping => ToTopping(topping) as OutputT,
            _ => null
        };
        return data;

    }
    public static Expression<Func<OutputT, bool>> ConvertToDataLayer<InputT, OutputT>(this Expression<Func<InputT, bool>> domainExpression) where InputT :Model.BaseDTO where OutputT : Entity
    {
        var parameter = Expression.Parameter(typeof(OutputT), "t");
        var body = new ReplaceParameterVisitor(domainExpression.Parameters[0], parameter).Visit(domainExpression.Body);

        return Expression.Lambda<Func<OutputT, bool>>(body, parameter);
    }
    public static OrderItem ToOrder(Model.OrderItem order)
    {
        var entity = new OrderItem()
        {
            Id = order.Id,
            Quantity = order.Quantity,
            Toppings= order.Toppings.Select(topping=>ToTopping(topping)).ToList(),
            PromotionId=order.Promotion.Id,
            TotalPrice=order.TotalPrice,
            ProductId= order.PizzaSize.Id

        };
        return entity;
    }

    public static Product ToProduct(Model.Product pizza)
    {
        var entity = new Product()
        {
            Id = pizza.Id,
            Price = pizza.Price,
            Size=(ProductSizeEnum)Enum.Parse(typeof (ProductSizeEnum),pizza.Name)
        };
        return entity;
    }

    
    public static Topping ToTopping(Model.Topping topping)
    {
        var entity = new Topping()
        {
            Id = topping.Id,
            Name = topping.Name,
            Price=topping.Price,
            Category= (ToppingCategoryEnum)Enum.Parse(typeof(ToppingCategoryEnum),topping.ToppingType.ToString())

        };
        return entity;
    }

    public static Promotion ToPromotion(Model.Promotion promotion)
    {
        var entity = new Promotion()
        {
            
            Id=promotion.Id,
            Discount=promotion.Discount,
            Name=promotion.Name,
            Price=promotion.Price,
            Quantity=promotion.Quantity,
            ProductSize=(ProductSizeEnum)Enum.Parse(typeof(ProductSizeEnum),promotion.Size),
            TotalToppings=promotion.TotalToppings,
            TotalToppingsUnit=promotion.TotalToppingsUnit?.Select(t=>ToToppingUnit(t))

        };
        return entity;
    }

    public static ToppingUnit ToToppingUnit(Model.ToppingUnit toppingUnit)
    {
            var entity = new ToppingUnit()
            {
                ToppingName=toppingUnit.ToppingName,
                Unit=toppingUnit.Unit
            };
            return entity;

    }


}
