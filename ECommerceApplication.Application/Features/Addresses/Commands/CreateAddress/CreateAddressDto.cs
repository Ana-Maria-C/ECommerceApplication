namespace ECommerceApplication.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressDto
    {
        public Guid AddressId { get; set; }
        public Guid UserId { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
    }
}

