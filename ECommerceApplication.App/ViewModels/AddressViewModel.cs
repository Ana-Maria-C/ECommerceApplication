using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.App.ViewModels
{
    public class AddressViewModel
    {
        public Guid AddressId { get; set; }

        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Postal code is required.")]
        public string PostalCode { get; set; } = string.Empty;

        public bool IsDefault { get; set; }
    }
}
