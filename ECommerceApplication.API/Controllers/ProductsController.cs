using ECommerceApplication.Application.Features.Products.Queries;
using ECommerceApplication.Application.Features.Products.Queries.GetAllProduct;
using ECommerceApplication.Application.Features.Products.Queries.GetByIdProduct;
using ECommerceApplication.Application.Features.Products.Commands.CreateProduct;
using ECommerceApplication.Application.Features.Products.Commands.DeleteProduct;
using ECommerceApplication.Application.Features.Products.Commands.UpdateProduct;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ECommerceApplication.Application.Features.Products.Queries.GetProductByCategoryId;

namespace ECommerceApplication.API.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        /*[Authorize(Roles = "User")]*/
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CreateProductCommandResponse>> Create([FromBody] CreateProductCommand createProductCommand)
        {
            var response = await Mediator.Send(createProductCommand);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            var dtos = await Mediator.Send(new GetAllProductQuery());
            return Ok(dtos);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateProductCommand updateProductCommand)
        {
            if (id != updateProductCommand.ProductId)
            {
                return BadRequest("The provided product ID does not match the request body.");
            }

            var result = await Mediator.Send(updateProductCommand);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteEventCommand = new DeleteProductCommand() { ProductId = id };
            await Mediator.Send(deleteEventCommand);
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdProductQuery()
            {
                ProductId = id
            });
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result.Product);
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<ActionResult<UpdateProductCommandResponse>> Update([FromBody] CreateProductCommand updateProductCommand)
        {
            var response = await Mediator.Send(updateProductCommand);
            return Ok(response);
        }

        [HttpGet("ByCategoryId/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductByCategoryId(Guid categoryId)
        {
            var query = new GetProductByCategoryIdQuery() { CategoryId = categoryId };
            var result = await Mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
