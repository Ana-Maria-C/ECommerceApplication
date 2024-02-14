using ECommerceApplication.Application.Features.OrderItems;
using ECommerceApplication.Domain.Entities;

namespace ECommerceApplication.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderDto
    {
        public Guid OrderId { get;  set; }
        public Guid UserId { get;  set; }
        public bool OrderPaid { get;  set; }
        public Payment Payment { get;  set; }
        public Guid AddressId { get;  set; }
        public Address Address { get;  set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}

