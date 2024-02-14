using ECommerceApplication.Domain.Entities;
using FluentAssertions;

// Import necessary namespaces

namespace ECommerceApplication.Domain.Tests
{
    public class AddressTests
    {
        [Fact]
        public void When_CreateAddressIsCalled_WithValidParameters_Then_SuccessIsReturned()
        {
            // Arrange && Act
            var result = Address.Create(Guid.NewGuid(),"123 Main St", "City", "State", "12345", true);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.AddressId.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void When_CreateAddressIsCalled_WithInvalidStreet_Then_FailureIsReturned()
        {
            // Arrange && Act
            var result = Address.Create(Guid.NewGuid(), "", "City", "State", "12345", true);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Street is required.");
        }

        [Fact]
        public void When_CreateAddressIsCalled_WithInvalidCity_Then_FailureIsReturned()
        {
            // Arrange && Act
            var result = Address.Create(Guid.NewGuid(), "123 Main St", "", "State", "12345", true);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("City is required.");
        }

        [Fact]
        public void When_CreateAddressIsCalled_WithInvalidState_Then_FailureIsReturned()
        {
            // Arrange && Act
            var result = Address.Create(Guid.NewGuid(), "123 Main St", "City", "", "12345", true);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("State is required.");
        }

        [Fact]
        public void When_CreateAddressIsCalled_WithInvalidPostalCode_Then_FailureIsReturned()
        {
            // Arrange && Act
            var result = Address.Create(Guid.NewGuid(), "123 Main St", "City", "State", "", true);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Postal code is required.");
        }

        [Fact]
        public void When_UpdateAddressIsCalled_WithValidParameters_Then_AddressIsUpdatedSuccessfully()
        {
            // Arrange
            var address = Address.Create(Guid.NewGuid(), "123 Main St", "City", "State", "12345", true);

            // Act
            address.Value.Update("456 New St", "New City", "New State", "54321", false);

            // Assert
            address.Value.Street.Should().Be("456 New St");
            address.Value.City.Should().Be("New City");
            address.Value.State.Should().Be("New State");
            address.Value.PostalCode.Should().Be("54321");
            address.Value.IsDefault.Should().BeFalse();
            address.IsSuccess.Should().BeTrue();
        }
    }
}
