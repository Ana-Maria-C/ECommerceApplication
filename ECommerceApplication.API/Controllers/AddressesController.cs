using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ECommerceApplication.Application.Features.Addresses.Commands.CreateAddress;
using ECommerceApplication.Application.Features.Addresses.Queries.GetAllAddress;
using ECommerceApplication.Application.Features.Addresses.Commands.UpdateAddress;
using ECommerceApplication.Application.Features.Addresses.Queries.GetAddressByUserId;
using ECommerceApplication.Application.Features.Users.Queries.GetByIdUser;
using ECommerceApplication.Application.Features.Addresses.Commands.DeleteAddress;

namespace ECommerceApplication.API.Controllers
{
    public class AddressesController : ApiControllerBase
    {
        //[Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateAddressCommand command)
        {
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
            var command = new GetAllAddressQuery();
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpGet("ByUserId/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAddressByUserId(Guid userId)
        {
            var query = new GetAddressByUserIdQuery { UserId = userId };
            var result = await Mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateAddressCommand updateAddressCommand)
        {
            if (id != updateAddressCommand.AddressId)
            {
                return BadRequest("The provided product ID does not match the request body.");
            }

            var result = await Mediator.Send(updateAddressCommand);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{addressId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid addressId)
        {
            var command = new DeleteAddressCommand { AddressId = addressId };
            var result = await Mediator.Send(command);
/*            if (!result.Success)
            {
                return NotFound(result);
            }*/
            return Ok(result);
        }
    }
}
