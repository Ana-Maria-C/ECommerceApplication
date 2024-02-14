using ECommerceApplication.API.Controllers;
using ECommerceApplication.Application.Features.Photos.Commands.AddPhotoToOwner;
using ECommerceApplication.Application.Features.Photos.Queries.GetPhotoForOwner;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApplication.Api.Controllers
{

    public class PhotosController : ApiControllerBase
    {
        //[Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddPhotoToTaskItem([FromForm] AddPhotoToOwnerCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        //[Authorize(Roles = "User")]
        [HttpGet("{ownerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPhotosForTaskItem(Guid ownerId)
        {
            var result = await Mediator.Send(new GetPhotoForOwnerQuery { OwnerId = ownerId });
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}