using ECommerceApplication.Domain.Common;

namespace ECommerceApplication.Domain.Entities
{
    public class OrderItem : AuditableEntity
    {
        public Guid OrderItemId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid? ShoppingCartId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal PricePerUnit { get; private set; }

        private OrderItem(Guid productId, int quantity, decimal pricePerUnit)
        {
            OrderItemId = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            PricePerUnit = pricePerUnit;
        }

        public static Result<OrderItem> Create(Guid productId, int quantity, decimal pricePerUnit)
        {
            if (productId == default)
            {
                return Result<OrderItem>.Failure("Product id is required.");
            }

            if (quantity <= 0)
            {
                return Result<OrderItem>.Failure("Quantity must be greater than zero.");
            }

            if (pricePerUnit <= 0)
            {
                return Result<OrderItem>.Failure("Price per unit must be greater than zero.");
            }

            return Result<OrderItem>.Success(new OrderItem(productId, quantity, pricePerUnit));
        }

        public void Update(Guid newProductId, int newQuantity, decimal newPricePerUnit)
        {
            ProductId = newProductId;
            Quantity = newQuantity;
            PricePerUnit = newPricePerUnit;
        }

        public decimal CalculateTotal()
        {
            return Quantity * PricePerUnit;
        }
    }
}
