using ECommerceApplication.Application.Features.Categories.Queries.GetByUserIdShoppingCart;
using ECommerceApplication.Application.Features.Order.Commands.CreateOrder;
using ECommerceApplication.Application.Features.Order.Queries.GetByFilterOrder;
using ECommerceApplication.Application.Features.ShoppingCarts.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApplication.API.Controllers
{
    public class OrderController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] Guid? userId)
        {
            var command = new GetByFilterOrderQuery { UserId = userId };
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
