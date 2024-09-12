
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.Domain.Mapping;
using PizzaOrderingSystem.Domain.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Entities;
using System.Linq.Expressions;

namespace PizzaOrderingSystem.Domain.Services;

public class OrderService : IOrderService
{

    private ILogger<OrderService> _logger;
    private IUnitOfWork _unitOfWork;
    private IPromotionService _promotionService;
    private IToppingService _toppingService;
    private IProductService _pizzaSizeService;
    public OrderService(IUnitOfWork unitOfWork,ILogger<OrderService> logger, IPromotionService promotionService, IToppingService toppingService,IProductService pizzaSizeService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _promotionService= promotionService;
        _toppingService= toppingService;
        _pizzaSizeService= pizzaSizeService;
    }

    public async Task SaveOrders(IEnumerable<Model.CreateOrder> orders)
    {
        try
        {
            var mergedOrderItemsWithSameFeatures= orders.GroupBy(order => string.Join(",", order.Toppings.OrderDescending())).Select(group => new Model.CreateOrder(group.First().PizzaSize, group.First().Toppings, (short)group.Sum(o => o.Quantity)));

            var allProducts = await _unitOfWork.Repository<Product>().GetAllAsync();
            var allToppings = await _unitOfWork.Repository<Topping>().GetAllAsync();
            var allPromotions = await _unitOfWork.Repository<Promotion>().GetAllAsync();
            var finalOrderItems=new List<OrderItem>();

            foreach (var createOrderItem in mergedOrderItemsWithSameFeatures) {
                var orderToppings = allToppings.Where(topping => createOrderItem.Toppings.Contains(topping.Name)).Select(topping => topping).ToList();
                //var orderToppings = await _unitOfWork.Repository<Topping>().FindAsync(topping => createOrderItem.Toppings.Contains(topping.Name));
                var orderPizzaSize = allProducts.FirstOrDefault(pizza =>createOrderItem.PizzaSize.Equals(pizza.Size.ToString(),StringComparison.OrdinalIgnoreCase));
                var matchedPromotion = await GetOrderMatchedPromotion(createOrderItem, allPromotions);
                finalOrderItems.Add(
                        new OrderItem()
                        {
                            Quantity= createOrderItem.Quantity,
                            ProductId= orderPizzaSize.Id,
                            PromotionId = matchedPromotion?.Id??null,
                            Toppings = orderToppings?.ToList(),
                            TotalPrice = CalculateOrderItemTotalPrice(createOrderItem,orderPizzaSize.Price, orderToppings, matchedPromotion)
                        }
                    );
                
            }

            var newOrder=new Order()
            {
                OrderItems = finalOrderItems,
                TotalPrice = finalOrderItems.Sum(order=>order.TotalPrice)
            };
            await _unitOfWork.Repository<Order>().AddAsync(newOrder);
            await _unitOfWork.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(SaveOrders)}");
            throw new Exception("There is a problem to save orders.", ex);
        }
    }


    private decimal CalculateOrderItemTotalPrice(Model.CreateOrder order,decimal pizzaPrice,IEnumerable<Topping> orderToppings, Promotion? promotion)
    {
        var toppingPrice=orderToppings.Sum(topping => topping.Price);
        var orderPrice = pizzaPrice + toppingPrice;
        var totalPrice = orderPrice;
        if (promotion !=null)
        {
            if(promotion.Quantity > 1)
            {
                decimal quantity=order.Quantity / promotion.Quantity;
                var numberOfItemsMatched = Math.Truncate(quantity);
                totalPrice =
                  (promotion.Price!=0
                    ? promotion.Price
                    : ((100 - (promotion.Discount??0)) * orderPrice) / 100) *
                  numberOfItemsMatched;
                var numberOfItemswithoutOffers =
                  order.Quantity - (numberOfItemsMatched * promotion.Quantity);
                totalPrice =
                  totalPrice +
                  numberOfItemswithoutOffers * orderPrice;
            }
            else
            {
                totalPrice =
                    (promotion.Price !=0
                      ? promotion.Price
                      : ((100 - (promotion.Discount??0)) * orderPrice) / 100) *
                    order.Quantity;

            }
            return totalPrice;

        }
        return orderPrice * order.Quantity;
    }

    public async Task<Promotion?> GetOrderMatchedPromotion(Model.CreateOrder order, IEnumerable<Promotion> promotions)
    {
        try
        {
            foreach (var promotion in promotions)
            {
                if (promotion.ProductSize.ToString().ToLower() != order.PizzaSize.ToLower()) continue;
                if (promotion.TotalToppings != 0)
                {
                    var orderToppingNo = order.Toppings.Count;
                    if (promotion.TotalToppingsUnit != null)
                    {
                        foreach (var item in promotion.TotalToppingsUnit)
                        {
                            if (order.Toppings.Contains(item.ToppingName))
                            {
                                orderToppingNo = orderToppingNo - 1 + item.Unit;
                            }
                        }
                    }
                    if (orderToppingNo == promotion.TotalToppings)
                    {
                        if (promotion.Quantity > 1)
                        {
                            if (order.Quantity >= promotion.Quantity)
                            {
                                return promotion;
                            }
                        }
                        else
                        {
                            return promotion;

                        }

                    }

                }
                else if (promotion.Quantity > 1 && order.Quantity == promotion.Quantity)
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
