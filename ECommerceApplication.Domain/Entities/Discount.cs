using ECommerceApplication.Domain.Common;

namespace ECommerceApplication.Domain.Entities
{
    public class Discount : AuditableEntity
    {
        public Guid DiscountId { get; private set; }
        public string Code { get; private set; }
        public decimal Percentage { get; private set; }
        public DateTime ExpiryDate { get; private set; }

        private Discount(string code, decimal percentage, DateTime expiryDate)
        {
            DiscountId = Guid.NewGuid();
            Code = code;
            Percentage = percentage;
            ExpiryDate = expiryDate;
        }

        public static Result<Discount> Create(string code, decimal percentage, DateTime expiryDate)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return Result<Discount>.Failure("Discount code is required.");
            }

            if (percentage <= 0 || percentage > 100)
            {
                return Result<Discount>.Failure("Percentage must be between 0 and 100.");
            }

            if (expiryDate < DateTime.UtcNow)
            {
                return Result<Discount>.Failure("Expiry date must be in the future.");
            }

            return Result<Discount>.Success(new Discount(code, percentage, expiryDate));
        }

        public bool IsExpired()
        {
            return DateTime.UtcNow > ExpiryDate;
        }

        public bool IsValidForOrder(DateTime orderDate)
        {
            return orderDate <= ExpiryDate;
        }

        public decimal CalculateDiscountAmount(decimal totalAmount)
        {
            return (Percentage / 100) * totalAmount;
        }

        public bool Equals(Discount other)
        {
            return other != null && DiscountId == other.DiscountId;
        }
    
    }
}
