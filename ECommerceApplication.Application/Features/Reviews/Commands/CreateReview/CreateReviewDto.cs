
namespace ECommerceApplication.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewDto
    {
        public Guid ReviewId { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string? ReviewText { get; set; }
        public decimal Rating { get; set; }
    }
}
