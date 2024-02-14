using ECommerceApplication.Domain.Entities;

namespace ECommerceApplication.Application.Features.Order
{
    public class OrderDto
    {
        public Guid OrderId { get;  set; }
        public Guid UserId { get;  set; }
        public bool OrderPaid { get;  set; }
        public Payment Payment { get;  set; }

        public Guid AddressId { get;  set; }
        public Address Address { get;  set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
