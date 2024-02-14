namespace ECommerceApplication.App.ViewModels
{
    public class ReviewDto
    {
        public Guid ReviewId { get; set; }
        public string ReviewText { get; set; } = string.Empty;
        public int Rating { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
