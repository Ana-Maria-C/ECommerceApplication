using ECommerceApplication.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Reviews.Queries.GetReviewByProductId
{
    public class GetReviewByProductIdQueryHandler:IRequestHandler<GetReviewByProductIdQuery,GetReviewByProductIdResponse>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetReviewByProductIdQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<GetReviewByProductIdResponse> Handle(GetReviewByProductIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _reviewRepository.GetReviewsByProductIdAsync(request.ProductId);
            if(!result.IsSuccess)
            {
                return new GetReviewByProductIdResponse
                {
                    Success = false,
                    Reviews = null
                };
            }
            var reviews = result.Value;
            if (reviews == null || !reviews.Any())
            {
                return new GetReviewByProductIdResponse
                {
                    Success = true,
                    Reviews = new List<ReviewDto>()
                };
            }
            var reviesDto=new List<ReviewDto>();
            foreach(var review in reviews)
            {
                reviesDto.Add(new ReviewDto
                {
                    ReviewId = review.ReviewId,
                    ProductId = review.ProductId,
                    UserId = review.UserId,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating
                });
            }
            return new GetReviewByProductIdResponse
            {
                Success = true,
                Reviews = reviesDto
            };
           
        }
    }
}
