using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.App.ViewModels
{
    public class OrderItemViewModel
    {
        public Guid OrderItemId { get; set; }
        [Required(ErrorMessage = "Product id is required")]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "PricePerUnit is required")]
        public decimal PricePerUnit { get; set; }
        public string StringProductId { get; set; } = string.Empty;

        public ProductViewModel? Product { get; set; }
        
        public decimal CalculateTotal()
        {
            return Quantity * PricePerUnit;
        }
        public static OrderItemViewModel FromDto(OrderItemDto dto)
        {
            return new OrderItemViewModel
            {
                OrderItemId = dto.OrderItemId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                PricePerUnit = dto.PricePerUnit,
                StringProductId = dto.ProductId.ToString()
            };
        }
    }
}
