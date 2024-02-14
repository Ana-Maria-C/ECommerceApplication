using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand: IRequest<CreateReviewCommandResponse>
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string? ReviewText { get; set; }
        public int Rating { get; set; } = 0;
    }
}
