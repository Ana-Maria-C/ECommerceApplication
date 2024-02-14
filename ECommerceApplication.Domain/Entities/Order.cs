using ECommerceApplication.Domain.Common;
using Stripe;

namespace ECommerceApplication.Domain.Entities
{

    public class Order : AuditableEntity
    {
        public Order()
        {
            // Required by Entity Framework Core
        }

        private Order(List<OrderItem> orderItems, Guid userId, Guid addressId)
        {
            AddressId = addressId;
            OrderId = Guid.NewGuid();
            UserId = userId;
            OrderItems = orderItems;
        }

        public Guid OrderId { get; private set; }
        public Guid UserId { get; private set; }
        public bool OrderPaid { get; private set; }
        public Payment Payment { get; private set; }

        public Guid AddressId { get; private set; }
        public Address Address { get; private set; }


        public List<OrderItem> OrderItems { get; private set; }

        public static Result<Order> Create(List<OrderItem> orderItems, Guid userId, Guid addressId)
        {
            if (orderItems.Count == 0)
            {
                return Result<Order>.Failure("Order items are required.");
            }
            if (userId == Guid.Empty)
            {
                return Result<Order>.Failure("User id should not be default");
            }

            return Result<Order>.Success(new Order(orderItems, userId, addressId));
        }


        public void AddPayment(Payment payment)
        {
            Payment = payment;
        }

        public void MarkAsPaid()
        {
            if (Payment != null && Payment.IsPaymentValid())
            {
                OrderPaid = true;
            }
        }

        public void CancelOrder()
        {
            if (OrderPaid)
            {
                throw new InvalidOperationException("Cannot cancel a paid order.");
            }
        }
    }
}
