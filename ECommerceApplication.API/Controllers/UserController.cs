using ECommerceApplication.Application.Features.Users.Commands.UpdateUser;
using ECommerceApplication.Application.Features.Users.Queries.GetByIdUser;
using ECommerceApplication.Application.Features.Users.Queries.GetAllUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerceApplication.Application.Features.Users.Queries.GetByEmailUser;

namespace ECommerceApplication.API.Controllers
{
    public class UserController : ApiControllerBase
    {
        //[Authorize(Roles = "User")]
        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid userId, UpdateUserCommand command)
        {
            // Ensure the userId in the path matches the one in the request body
            if (userId != command.Id)
            {
                return BadRequest("The provided userId in the path does not match the one in the request body.");
            }

            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        //[Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllUserQuery());
            return Ok(result);
        }

        /*[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUserCommand { UserId = id };
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }*/

        //[Authorize(Roles = "User")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById(string id)
        {
            var query = new GetByIdUserQuery { UserId = id };
            var result = await Mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("ByEmail/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var query = new GetByEmailUserQuery { Email = email };
            var result = await Mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}