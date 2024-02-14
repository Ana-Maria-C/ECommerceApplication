namespace ECommerceApplication.App.ViewModels
{
    public class OrderViewModel
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public bool OrderPaid { get; set; }
        //public Payment Payment { get; set; }

        public Guid AddressId { get; set; }
        public AddressViewModel Address { get; set; }
        public List<OrderItemViewModel>? OrderItems { get; set; }
    }
}
