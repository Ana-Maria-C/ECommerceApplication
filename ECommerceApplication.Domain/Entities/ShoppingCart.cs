using ECommerceApplication.Domain.Common;
using Stripe.Treasury;

namespace ECommerceApplication.Domain.Entities
{
    public class ShoppingCart : AuditableEntity
    {
        private ShoppingCart(Guid userId)
        {
            used = false;
            ShoppingCartId = Guid.NewGuid();
            UserId = userId;
            OrderItems = new List<OrderItem>();
        }

        public Guid ShoppingCartId { get; private set; }
        public Guid UserId { get; private set; }
        public bool used { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }
        public static Result<ShoppingCart> Create(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return Result<ShoppingCart>.Failure("User id is required.");
            }
            return Result<ShoppingCart>.Success(new ShoppingCart(userId));
        }
        public void MarkAsUsed()
        {
            used = true;
        }

        public void AddProduct(OrderItem orderItem)
        {
            OrderItems.Add(orderItem);
        }

        public void RemoveProduct(Guid orderItemId)
        {
            var orderItemToRemove = OrderItems.Find(orderItem => orderItem.OrderItemId == orderItemId);

            if (orderItemToRemove != null)
            {
                OrderItems.Remove(orderItemToRemove);
            }
        }

        public decimal CalculateTotal()
        {
            return OrderItems.Aggregate((decimal) 0, (acc, x) => acc + x.CalculateTotal());
        }
    }
}
