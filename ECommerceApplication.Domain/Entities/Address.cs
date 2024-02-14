using ECommerceApplication.Domain.Common;

namespace ECommerceApplication.Domain.Entities
{
    public class Address : AuditableEntity
    {
        private Address(Guid userId, string street, string city, string state, string postalCode, bool isDefault)
        {
            AddressId = Guid.NewGuid();
            UserId = userId;
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            IsDefault = isDefault;
        }

        public Guid AddressId { get; private set; }
        public Guid UserId { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }
        public bool IsDefault { get; private set; }

        public static Result<Address> Create(Guid userId, string street, string city, string state, string postalCode, bool isDefault)
        {
            if (string.IsNullOrWhiteSpace(street))
            {
                return Result<Address>.Failure("Street is required.");
            }
            if (string.IsNullOrWhiteSpace(city))
            {
                return Result<Address>.Failure("City is required.");
            }
            if (string.IsNullOrWhiteSpace(state))
            {
                return Result<Address>.Failure("State is required.");
            }
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                return Result<Address>.Failure("Postal code is required.");
            }

            return Result<Address>.Success(new Address(userId, street, city, state, postalCode, isDefault));
        }

        public void Update(string street, string city, string state, string postalCode, bool isDefault)
        {
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            IsDefault = isDefault;
        }

        /*public bool MarkAsDefault()
        {
            if (!IsDefault)
            {
                IsDefault = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UnmarkAsDefault()
        {
            if (IsDefault)
            {
                IsDefault = false;
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool IsDefaultAddress()
        {
            return IsDefault;
        }

        public string GetFormattedAddress()
        {
            return $"{Street}, {City}, {State} {PostalCode}";
        }*/
    }
}
