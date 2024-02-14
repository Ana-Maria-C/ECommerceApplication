using Xunit;
using NSubstitute;
using ECommerceApplication.Application.Features.Addresses.Commands.UpdateAddress;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using ECommerceApplication.Application.Responses;
using ECommerceApplication.Domain.Common;

namespace ECommerceApplication.Application.Tests.Commands.AddressTests
{
    public class UpdateAddressCommandHandlerTests
    {
        private readonly IAddressRepository _repository;
        private readonly UpdateAddressCommandHandler _handler;

        public UpdateAddressCommandHandlerTests()
        {
            _repository = Substitute.For<IAddressRepository>();
            _handler = new UpdateAddressCommandHandler(_repository);
        }

        [Fact]
        public async Task Handle_InvalidInput_ReturnsFailure()
        {
            // Arrange
            var invalidCommand = new UpdateAddressCommand { AddressId = Guid.NewGuid(), Street = "", City = "", State = "", PostalCode = "" }; // Invalid due to empty fields

            // Act
            var response = await _handler.Handle(invalidCommand, new CancellationToken());

            // Assert
            Assert.False(response.Success);
            Assert.NotNull(response.ValidationsErrors);
            Assert.NotEmpty(response.ValidationsErrors);
        }

        [Fact]
        public async Task Handle_AddressNotFound_ReturnsFailure()
        {
            // Arrange
            var command = new UpdateAddressCommand { AddressId = new Guid(), Street = "TestStreet", City = "TestCity", State = "TestState", PostalCode = "TestPostalCode" };
            _repository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult<Result<Address>>(Result<Address>.Failure("Address not found")));

            // Act
            var response = await _handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(response.Success);
            Assert.Contains("Address not found", response.ValidationsErrors);
        }

        [Fact]
        public async Task Handle_ValidInput_ReturnsSuccess()
        {
            // Arrange
            var addressId = Guid.NewGuid();
            var mockAddress = Address.Create(Guid.NewGuid(), "TestStreet", "TestCity", "TestState", "TestPostalCode", false).Value;
            var validCommand = new UpdateAddressCommand
            {
                AddressId = mockAddress.AddressId,
                Street = "UpdatedStreet",
                City = "UpdatedCity",
                State = "UpdatedState",
                PostalCode = "UpdatedPostalCode",
                IsDefault = true
            };
            _repository.FindByIdAsync(mockAddress.AddressId)
                .Returns(Task.FromResult<Result<Address>>(Result<Address>.Success(mockAddress)));
            _repository.UpdateAsync(Arg.Any<Address>())
                .Returns(Task.FromResult<Result<Address>>(Result<Address>.Success(mockAddress)));

            // Act
            var response = await _handler.Handle(validCommand, new CancellationToken());

            // Assert
            Assert.True(response.Success);
            Assert.Equal("UpdatedStreet", response.Street);
            Assert.Equal("UpdatedCity", response.City);
            Assert.Equal("UpdatedState", response.State);
            Assert.Equal("UpdatedPostalCode", response.PostalCode);
            Assert.True(response.IsDefault);
        }

        [Fact]
        public async Task Handle_UpdateFailure_ReturnsFailure()
        {
            // Arrange
            var addressId = Guid.NewGuid();
            var command = new UpdateAddressCommand
            {
                AddressId = addressId,
                Street = "TestStreet",
                City = "TestCity",
                State = "TestState",
                PostalCode = "TestPostalCode",
                IsDefault = false
            };
            var mockAddress = Address.Create(Guid.NewGuid(), "TestStreet", "TestCity", "TestState", "TestPostalCode", false).Value;
            _repository.FindByIdAsync(addressId)
                .Returns(Task.FromResult<Result<Address>>(Result<Address>.Success(mockAddress)));

            // Act
            var response = await _handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(response.Success);
            Assert.Contains("IsDefault is required.", response.ValidationsErrors);
        }
    }
}
