using ECommerceApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Payments.Commands.UpdatePayment
{
    public class UpdatePaymentCommandResponse : BaseResponse
    {
        public UpdatePaymentCommandResponse() : base()
        {
        }

        public UpdatePaymentDto Payment { get; set; } = default!;
    }
}
