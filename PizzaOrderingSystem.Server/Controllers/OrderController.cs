using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Domain.Model;
using PizzaOrderingSystem.Domain.Model.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzaOrderingSystem.Server.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IEnumerable<CreateOrder> orders)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            await _orderService.SaveOrders(orders);

            return Ok();
        }
    }
}
