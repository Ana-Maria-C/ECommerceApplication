using ECommerceApplication.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler:IRequestHandler<DeleteReviewCommand, DeleteReviewCommandResponse>
    {
        private readonly IReviewRepository _reviewRepository;

        public DeleteReviewCommandHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<DeleteReviewCommandResponse> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var result =await _reviewRepository.DeleteAsync(request.ReviewId);
            if(!result.IsSuccess)
            {
                return new DeleteReviewCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }
            else
            {
                return new DeleteReviewCommandResponse
                {
                    Success = true
                };
            }
        }
    }
}
