using Microsoft.AspNetCore.Mvc;
using PizzaOrderSystem.DataAccess.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Entities;
using PizzaOrderSystem.DataAccess.Model.Enumerations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzaOrderingSystem.Server.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public PromotionController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAllPromotionsInMemory()
        {
            var promotions = new List<Promotion>() { new Promotion()

                {
                    Id = Guid.NewGuid(),
                    Price = 5,
                    Name = "Offer1",
                    ProductSize = ProductSizeEnum.Medium,
                    TotalToppings = 2,
                },
                new Promotion()
                {
                    Id = Guid.NewGuid(),
                    Price = 9,
                    Name = "Offer2",
                    ProductSize = ProductSizeEnum.Medium,
                    TotalToppings = 4,
                    Quantity = 2
                },
                new Promotion()
                {
                    Id = Guid.NewGuid(),
                    Price = 0,
                    Discount = 50,
                    Name = "Offer3",
                    ProductSize = ProductSizeEnum.Large,
                    TotalToppings = 4
                }};
           await _unitOfWork.Repository<Promotion>().AddAllAsync(promotions);
           await _unitOfWork.Repository<ToppingUnit>().AddAsync(new ToppingUnit()
           {
               Id = Guid.NewGuid(),
               ToppingName = "Pepperoni",
               PromotionId = promotions[2].Id,
               Unit = 2,
           });
            return Ok();
        }
    }
}
