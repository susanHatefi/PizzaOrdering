using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Domain.Model;
using PizzaOrderingSystem.Domain.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzaOrderingSystem.Server.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        private IToppingService _toppinService;
        private IPromotionService _promotionService;
        private IUnitOfWork _unitOfWork;
        public ProductController(IProductService productService, IToppingService toppingService, IPromotionService promotionService, IUnitOfWork unitOfWork)
        {
            _productService = productService;
            _toppinService = toppingService;
            _promotionService = promotionService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public  async Task<IActionResult> GetFeatures()
        {
            var products = await _productService.GetAllProducts();
            var (VegTopping, NonVegTopping) = await _toppinService.GetToppingsSeperatedByType();
            var promotions=await _promotionService.GetAllPromotions();
            var productFeature=new ProductFeature()
            {
                PizzaSize =products.OrderByDescending(p=>p.Name),
                VegToppings=VegTopping,
                NonVegToppings= NonVegTopping,
                Promotion= promotions,

            };
            return Ok(productFeature);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProductsInMemmory()
        {
            var products=new List<PizzaOrderSystem.DataAccess.Model.Entities.Product>()
            {
                new PizzaOrderSystem.DataAccess.Model.Entities.Product()
                {
                    Id=Guid.NewGuid(),
                    Price=5M,
                    Size= PizzaOrderSystem.DataAccess.Model.Enumerations.ProductSizeEnum.Small
                },
                new PizzaOrderSystem.DataAccess.Model.Entities.Product()
                {
                    Id=Guid.NewGuid(),
                    Price=7M,
                    Size= PizzaOrderSystem.DataAccess.Model.Enumerations.ProductSizeEnum.Medium
                },
                new PizzaOrderSystem.DataAccess.Model.Entities.Product()
                {
                    Id=Guid.NewGuid(),
                    Price=8M,
                    Size= PizzaOrderSystem.DataAccess.Model.Enumerations.ProductSizeEnum.Large
                },
                new PizzaOrderSystem.DataAccess.Model.Entities.Product()
                {
                    Id=Guid.NewGuid(),
                    Price=9M,
                    Size= PizzaOrderSystem.DataAccess.Model.Enumerations.ProductSizeEnum.ExtraLarge
                },
            };
            await _unitOfWork.Repository<PizzaOrderSystem.DataAccess.Model.Entities.Product>().AddAllAsync(products);
            
            return Ok();
        }
    }
}
