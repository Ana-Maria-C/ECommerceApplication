using ECommerceApplication.Domain.Common;

namespace ECommerceApplication.Domain.Entities
{
    public class Review
    {
        private Review(Guid reviewId, Guid productId, Guid userId, string reviewText, decimal rating)
        {
            ReviewId = reviewId;
            ProductId = productId;
            UserId = userId;
            ReviewText = reviewText;
            Rating = rating;
        }
        public Guid ReviewId { get; set; } 
        public Guid ProductId { get; set;} 
        public Guid UserId { get; set; }
        public string ReviewText { get; set; } 
        public decimal Rating { get; set; }    
        public static Result<Review> Create(Guid productId, Guid userId, string reviewText, decimal rating)
        {
            if (productId == default)
            {
                return Result<Review>.Failure("Product id is required.");
            }
            if (userId == default)
            {
                return Result<Review>.Failure("User id is required.");
            }
            if (string.IsNullOrWhiteSpace(reviewText))
            {
                return Result<Review>.Failure("Review text is required.");
            }
            if (rating == default)
            {
                return Result<Review>.Failure("Rating is required.");
            }
            return Result<Review>.Success(new Review(Guid.NewGuid(), productId, userId, reviewText, rating));
        }

        public void UpdateReview(string newReviewText, decimal newRating)
        {
            ReviewText = newReviewText;
            Rating = newRating;
        }
    }
}
