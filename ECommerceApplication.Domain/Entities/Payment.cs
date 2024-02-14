using ECommerceApplication.Domain.Common;

namespace ECommerceApplication.Domain.Entities
{
    public class Payment : AuditableEntity
    {
        private Payment(Guid paymentId, Guid orderId, decimal amount, DateTime paymentDate, string paymentMethod)
        {
            PaymentId = paymentId;
            OrderId = orderId;
            Amount = amount;
            PaymentDate = paymentDate;
            PaymentMethod = paymentMethod;
            PaymentStatus = "Pending";
            Currency = "RON";
        }

        public Guid PaymentId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public string PaymentMethod { get; private set; }
        public string Currency { get; private set; }
        public string PaymentStatus { get; private set; }

        public static Result<Payment> Create(Guid orderId, decimal amount, DateTime paymentDate, string paymentMethod)
        {
            if (orderId == default)
            {
                return Result<Payment>.Failure("Order id is required.");
            }
            if (amount <=0)
            {
                return Result<Payment>.Failure("Amount should be greater than zero.");
            }
            if (paymentDate == default)
            {
                return Result<Payment>.Failure("Payment date is required.");
            }
            if (string.IsNullOrWhiteSpace(paymentMethod))
            {
                return Result<Payment>.Failure("Payment method is required.");
            }
            return Result<Payment>.Success(new Payment(Guid.NewGuid(), orderId, amount, paymentDate, paymentMethod));
        }

        public bool IsPaymentValid()
        {
            if (PaymentStatus == "Paid")
            {
                return true;
            }
            return false;
        }

        public void MarkAsPaid()
        {
            if (PaymentStatus == "Pending")
            {
                PaymentStatus = "Paid";
            }
            else
            {
                throw new InvalidOperationException("Payment has already been processed or is invalid.");
            }
        }

        public void MarkAsFailed()
        {
            if (PaymentStatus == "Pending")
            {
                PaymentStatus = "Failed";
            }
            else
            {
                throw new InvalidOperationException("Payment has already been processed or is invalid.");
            }
        }

        public void UpdatePaymentMethod(string newPaymentMethod)
        {
            if (string.IsNullOrWhiteSpace(newPaymentMethod))
            {
                throw new ArgumentException("New payment method is required.", nameof(newPaymentMethod));
            }

            PaymentMethod = newPaymentMethod;
        }

        public void UpdatePaymentAmount(decimal newAmount)
        {
            if (newAmount == default)
            {
                throw new ArgumentException("New amount is required.", nameof(newAmount));
            }

            Amount = newAmount;
        }

        public void AttachPaymentStatus(string v)
        {
            if (!string.IsNullOrWhiteSpace(v))
            {
                PaymentStatus = v;
            }
        }

        public void AttachPaymentDate(DateTime now)
        {
            if(now != default)
            {
                PaymentDate = now;
            }
        }

        public void AttachPaymentMethod(string paymentMethod)
        {
            if(paymentMethod != null)
            {
                PaymentMethod = paymentMethod;
            }
        }

        public void AttachCurrency(string currency)
        {
            if (!string.IsNullOrWhiteSpace(currency))
            {
                Currency = currency;
            }
        }

        public void AttachAmount(decimal amount)
        {
            if (amount >= 0)
            {
                Amount=amount;
            }
        }

        public void AttachOrderId(Guid orderId)
        {
            if (orderId != Guid.Empty)
            {
                OrderId = orderId;
            }
        }

        public void Update(Guid orderId, decimal amount, DateTime paymentDate, string paymentMethod, string paymentStatus, string currency)
        {
            if (orderId == default)
            {
                throw new ArgumentException("Order id is required.", nameof(orderId));
            }
            if (amount == default)
            {
                throw new ArgumentException("Amount is required.", nameof(amount));
            }
            if (paymentDate == default)
            {
                throw new ArgumentException("Payment date is required.", nameof(paymentDate));
            }
            if (string.IsNullOrWhiteSpace(paymentMethod))
            {
                throw new ArgumentException("Payment method is required.", nameof(paymentMethod));
            }
            if (string.IsNullOrWhiteSpace(paymentStatus))
            {
                throw new ArgumentException("Payment status is required.", nameof(paymentStatus));
            }
            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new ArgumentException("Currency is required.", nameof(currency));
            }

            OrderId = orderId;
            Amount = amount;
            PaymentDate = paymentDate;
            PaymentMethod = paymentMethod;
            PaymentStatus = paymentStatus;
            Currency = currency;
        }
    }
}
