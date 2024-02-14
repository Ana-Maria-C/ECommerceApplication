using ECommerceApplication.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Reviews.Queries.GetAllReview
{
    public class GetAllReviewQueryHandler:IRequestHandler<GetAllReviewQuery,GetAllReviewQueryResponse>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetAllReviewQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<GetAllReviewQueryResponse> Handle(GetAllReviewQuery request, CancellationToken cancellationToken)
        {
            GetAllReviewQueryResponse response = new();
            var result = await _reviewRepository.GetAllAsync();
            response.Reviews=result.Value.Select(r => new ReviewDto
            {
                ReviewId = r.ReviewId,
                ProductId = r.ProductId,
                UserId = r.UserId,
                ReviewText = r.ReviewText,
                Rating = r.Rating
            }).ToList();
            return response;
        }
    }
}
