using Xunit;
using NSubstitute;
using ECommerceApplication.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Application.Features.Addresses.Commands.CreateAddress;
using ECommerceApplication.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace ECommerceApplication.Application.Tests.Commands.AddressTests
{
    public class CreateAddressCommandHandlerTests
    {
        private readonly IAddressRepository _repository;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateAddressCommandHandler> _logger;
        private readonly CreateAddressCommandHandler _handler;

        public CreateAddressCommandHandlerTests()
        {
            _repository = Substitute.For<IAddressRepository>();
            _emailService = Substitute.For<IEmailService>();
            _logger = Substitute.For<ILogger<CreateAddressCommandHandler>>();
            _handler = new CreateAddressCommandHandler(_repository, _emailService, _logger);
        }

        [Fact]
        public async Task Handle_InvalidInput_ReturnsFailure()
        {
            var invalidCommand = new CreateAddressCommand { Street = "", City = "City", State = "State", PostalCode = "12345" };
            var response = await _handler.Handle(invalidCommand, new CancellationToken());
            Assert.False(response.Success);
            Assert.NotNull(response.ValidationsErrors);
            Assert.NotEmpty(response.ValidationsErrors);
        }

        [Fact]
        public async Task Handle_ValidInput_ReturnsSuccess()
        {
            // Arrange
            var validCommand = new CreateAddressCommand { Street = "123 Street", City = "City", State = "State", PostalCode = "12345", IsDefault = true };

            // Act
            var response = await _handler.Handle(validCommand, new CancellationToken());

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response.Address);
            Assert.Equal("123 Street", response.Address.Street);
            Assert.True(response.Address.IsDefault);
        }

        [Fact]
        public async Task Handle_AddressCreationFailure_ReturnsFailure()
        {
            string invalidStreet = "";
            var commandWithCreationIssue = new CreateAddressCommand { Street = invalidStreet, City = "City", State = "State", PostalCode = "12345" };
            var response = await _handler.Handle(commandWithCreationIssue, new CancellationToken());
            Assert.False(response.Success);
            Assert.NotNull(response.ValidationsErrors);
            Assert.NotEmpty(response.ValidationsErrors);
        }

        [Fact]
        public async Task Handle_RepositoryAddFailure_ReturnsFailure()
        {
            var validCommand = new CreateAddressCommand { Street = "123 Street", City = "City", State = "State", PostalCode = "12345", IsDefault = true };
            _repository.When(r => r.AddAsync(Arg.Any<Address>())).Do(x => { throw new Exception("Database error"); });
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(validCommand, new CancellationToken()));
            Assert.Equal("Database error", exception.Message);
        }
    }
}
