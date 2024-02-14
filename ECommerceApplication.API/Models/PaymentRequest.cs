using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.API.Models
{
    public class PaymentRequest
    {
        [Required(ErrorMessage = "Order ID is required.")]
        public Guid OrderId { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Payment date is required.")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        public string PaymentMethod { get; set; }

    }
}
