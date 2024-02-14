using ECommerceApplication.Application.Features.Products.Commands.CreateProduct;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler:IRequestHandler<CreateReviewCommand, CreateReviewCommandResponse>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ILogger<CreateProductCommandHandler> logger;

       
        public CreateReviewCommandHandler(IReviewRepository reviewRepository, ILogger<CreateProductCommandHandler> logger)
        {
            _reviewRepository = reviewRepository;
            this.logger = logger;
        }

        public async Task<CreateReviewCommandResponse> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateReviewCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if(!validatorResult.IsValid)
            {
                return new CreateReviewCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

           
            var @review = Review.Create(request.ProductId, request.UserId, request.ReviewText, request.Rating);
            if(@review.IsSuccess)
            {
                await _reviewRepository.AddAsync(@review.Value);
                return new CreateReviewCommandResponse
                {
                    Success = true,
                    Review = new CreateReviewDto
                    {
                        ReviewId = @review.Value.ReviewId,
                        ProductId = @review.Value.ProductId,
                        UserId = @review.Value.UserId,
                        ReviewText = @review.Value.ReviewText,
                        Rating = @review.Value.Rating
                    }
                };
            }
            else
            {
                return new CreateReviewCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Review could not be created." }
                };
            }
        }

    }
}
