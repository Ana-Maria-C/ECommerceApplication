using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerceApplication.Application.Features.Addresses.Queries;
using ECommerceApplication.Application.Features.Addresses.Queries.GetAllAddress;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Entities;
using ECommerceApplication.Domain.Common;
using NSubstitute;
using Xunit;

namespace ECommercerApplication.Application.Tests.Queries.AddressTests
{
    public class GetAllAddressQueryHandlerTests
    {
        private readonly IAddressRepository _repository;
        private readonly GetAllAddressQueryHandler _handler;

        public GetAllAddressQueryHandlerTests()
        {
            _repository = Substitute.For<IAddressRepository>();
            _handler = new GetAllAddressQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_ReturnsAllAddresses()
        {
            // Arrange
            var addresses = new List<Address>
            {
                Address.Create(Guid.NewGuid(), "Street 1","City 1","State 1", "12345",true ).Value,
                Address.Create(Guid.NewGuid(), "Street 2", "City 2", "State 2", "54321", false).Value
                // Add more addresses as needed
            };
            var result = Result<IReadOnlyList<Address>>.Success(addresses);
            _repository.GetAllAsync().Returns(Task.FromResult<Result<IReadOnlyList<Address>>>(result));

            // Act
            var response = await _handler.Handle(new GetAllAddressQuery(), new CancellationToken());

            // Assert
            Assert.NotNull(response.Addresses);
            Assert.Equal(addresses.Count, response.Addresses.Count);
            Assert.All(response.Addresses, addressDto =>
                Assert.Contains(addresses, a =>
                    a.AddressId == addressDto.AddressId &&
                    a.Street == addressDto.Street &&
                    a.City == addressDto.City &&
                    a.State == addressDto.State &&
                    a.PostalCode == addressDto.PostalCode &&
                    a.IsDefault == addressDto.IsDefault));
        }

        // Additional test cases as needed...
    }
}
