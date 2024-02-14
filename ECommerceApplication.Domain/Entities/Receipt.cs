using ECommerceApplication.Domain.Common;

namespace ECommerceApplication.Domain.Entities
{
    public class Receipt : AuditableEntity
    {
        public Guid ReceiptId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateTime IssueDate { get; private set; }
        public List<Discount> AppliedDiscounts { get; private set; }

        private Receipt(Guid orderId, decimal totalAmount, DateTime issueDate, List<Discount> appliedDiscounts)
        {
            ReceiptId = Guid.NewGuid();
            OrderId = orderId;
            TotalAmount = totalAmount;
            IssueDate = issueDate;
            AppliedDiscounts = appliedDiscounts;
        }

        public static Result<Receipt> Create(Guid orderId, decimal totalAmount, DateTime issueDate, List<Discount> appliedDiscounts)
        {
            if (orderId == default)
            {
                return Result<Receipt>.Failure("Order id is required.");
            }

            if (totalAmount <= 0)
            {
                return Result<Receipt>.Failure("Total amount must be greater than zero.");
            }

            if (issueDate == default || issueDate > DateTime.UtcNow)
            {
                return Result<Receipt>.Failure("Invalid issue date.");
            }

            return Result<Receipt>.Success(new Receipt(orderId, totalAmount, issueDate, appliedDiscounts));
        }

        public void AddDiscount(Discount discount)
        {
            if(! AppliedDiscounts.Contains (discount))
            {
                AppliedDiscounts.Add(discount);
            }
        }

        public bool IsDiscountApplied(Discount discount)
        {
            return AppliedDiscounts?.Contains(discount) ?? false;
        }

        public void RemoveDiscount(Discount discount)
        {
            AppliedDiscounts?.Remove(discount);
        }

        public void UpdateTotalAmount(decimal newTotalAmount)
        {
            if (newTotalAmount >= 0)
            {
                TotalAmount = newTotalAmount;
            }
        }
    }
}
