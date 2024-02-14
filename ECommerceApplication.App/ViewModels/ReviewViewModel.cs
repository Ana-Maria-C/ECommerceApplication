using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.App.ViewModels
{
    public class ReviewViewModel
    {
        public Guid ReviewId { get; set; }
        public Guid ProductId { get; set; }
        public string ReviewText { get; set; } = string.Empty;
        [Required(ErrorMessage = "Rating is required!")]
        public int Rating { get; set; }
        public Guid UserId { get; set; }
    }
}
