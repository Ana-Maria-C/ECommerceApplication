using ECommerceApplication.Application.Features.Reviews.Commands.CreateReview;
using ECommerceApplication.Application.Features.Reviews.Commands.DeleteReview;
using ECommerceApplication.Application.Features.Reviews.Queries;
using ECommerceApplication.Application.Features.Reviews.Queries.GetAllReview;
using ECommerceApplication.Application.Features.Reviews.Queries.GetReviewByProductId;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApplication.API.Controllers
{
    public class ReviewsController:ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult>Create(CreateReviewCommand command)
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
        public async Task<IActionResult> GetAll()
        {
            var command =new GetAllReviewQuery();
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("ByProductId/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByProductId(Guid productId)
        {
            var command = new GetReviewByProductIdQuery { ProductId = productId };
            var result = await Mediator.Send(command);

                // Verifică dacă există recenzii și trimite un răspuns OK cu lista de recenzii
                if (result.Reviews == null || !result.Reviews.Any())
                {
                    return Ok(new List<ReviewDto>());
                }
                return Ok(result.Reviews);
            

            // Dacă rezultatul nu este de succes, întoarce un BadRequest cu detaliile erorii
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteReviewCommand { ReviewId = id };
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
