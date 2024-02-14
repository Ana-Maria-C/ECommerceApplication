using ECommerceApplication.Application.Features.Categories.Commands.CreateCategory;
using ECommerceApplication.Application.Features.Categories.Queries.GetAllCategory;
using Microsoft.AspNetCore.Mvc;
using ECommerceApplication.Application.Features.Categories.Queries.GetByIdCategory;
using ECommerceApplication.Application.Features.Categories.Queries.DeleteCategory;
using ECommerceApplication.Application.Features.Categories.Commands.UpdateCategory;

namespace ECommerceApplication.API.Controllers
{
    public class CategoriesController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid categoryId, UpdateCategoryCommand command)
        {
            // Ensure the categoryId in the path matches the one in the request body
            if (categoryId != command.CategoryId)
            {
                return BadRequest("The provided categoryId in the path does not match the one in the request body.");
            }

            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllCategoryQuery());
            return Ok(result.Categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetByIdCategoryQuery), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid categoryId)
        {
            var command = new GetByIdCategoryQuery { CategoryId = categoryId };
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeleteCategoryQuery), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            var command = new DeleteCategoryQuery { CategoryId = categoryId };
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
