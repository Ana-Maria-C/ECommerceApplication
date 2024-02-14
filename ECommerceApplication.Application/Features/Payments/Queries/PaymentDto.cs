namespace ECommerceApplication.Application.Features.Payments.Queries
{
    public class PaymentDto
    {
        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
            
        public string PaymentMethod { get; set; }
        public string Currency { get; set; }
        public string PaymentStatus { get; set; }
    }
}
