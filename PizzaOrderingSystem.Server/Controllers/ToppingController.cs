using Microsoft.AspNetCore.Mvc;
using PizzaOrderSystem.DataAccess.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Entities;
using PizzaOrderSystem.DataAccess.Model.Enumerations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzaOrderingSystem.Server.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ToppingController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public ToppingController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAllToppingsInMemmory()
        {
            var toppings = new List<Topping>() {
                new Topping()
            {
                Category = ToppingCategoryEnum.Veg,
                Id = Guid.NewGuid(),
                Name = "Tomatoes",
                Price = 1

            },
                        new Topping()
                        {
                            Category = ToppingCategoryEnum.Veg,
                            Id = Guid.NewGuid(),
                            Name = "Onions",
                            Price = 0.5M

                        },
                        new Topping()
                        {
                            Category = ToppingCategoryEnum.Veg,
                            Id = Guid.NewGuid(),
                            Name = "Bell Pepper",
                            Price = 1

                        },
                        new Topping()
                        {
                            Category = ToppingCategoryEnum.Veg,
                            Id = Guid.NewGuid(),
                            Name = "Mushrooms",
                            Price = 1.2M

                        },
                        new Topping()
                        {
                            Category = ToppingCategoryEnum.Veg,
                            Id = Guid.NewGuid(),
                            Name = "Pineapple",
                            Price = 0.75M

                        },
                        new Topping()
                        {
                            Category = ToppingCategoryEnum.NonVeg,
                            Id = Guid.NewGuid(),
                            Name = "Sausage",
                            Price = 1

                        },
                        new Topping()
                        {
                            Category = ToppingCategoryEnum.NonVeg,
                            Id = Guid.NewGuid(),
                            Name = "Pepperoni",
                            Price = 2

                        },
                        new Topping()
                        {
                            Category = ToppingCategoryEnum.NonVeg,
                            Id = Guid.NewGuid(),
                            Name = "Barbecue Chicken",
                            Price = 3

                        }

        
    };
            await _unitOfWork.Repository<Topping>().AddAllAsync(toppings);
            return Ok();
        }
    }
}
