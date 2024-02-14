using ECommerceApplication.API.Models;
using ECommerceApplication.Application.Contracts.Interfaces;
using ECommerceApplication.Application.Features.Payments.Commands.CreatePayment;
using ECommerceApplication.Application.Features.Payments.Commands.DeletePayment;
using ECommerceApplication.Application.Features.Payments.Commands.UpdatePayment;
using ECommerceApplication.Application.Features.Payments.Queries.GetAllPayment;
using ECommerceApplication.Application.Features.Payments.Queries.GetByIdPayment;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApplication.API.Controllers
{
   public class PaymentController :ApiControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetAllPaymentQuery();
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("{paymentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetByIdPaymentQuery), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid paymentId)
        {
            var command = new GetByIdPaymentQuery { PaymentId = paymentId };
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{paymentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeletePaymentCommand), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid paymentId)
        {
            var command = new DeletePaymentCommand { PaymentId = paymentId };
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreatePaymentCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{paymentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid paymentId, UpdatePaymentCommand command)
        {
            // Ensure the paymentId in the path matches the one in the request body
            if (paymentId != command.PaymentId)
            {
                return BadRequest("The provided paymentId in the path does not match the one in the request body.");
            }

            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
      
   }

}
