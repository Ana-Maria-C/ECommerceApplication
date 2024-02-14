using ECommerceApplication.Application.Features.Users.Queries;
using ECommerceApplication.Application.Features.Users.Queries.GetAllUser;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Common;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercerApplication.Application.Tests.Queries
{
    public class GetAllUserQueryHandlerTests
    {
        private readonly IUserManager _userRepository;
        private readonly GetAllUserQueryHandler _handler;

        public GetAllUserQueryHandlerTests()
        {
            _userRepository = Substitute.For<IUserManager>();
            _handler = new GetAllUserQueryHandler(_userRepository);
        }

        [Fact]
        public async Task Handle_WhenCalled_ReturnsAllUsers()
        {
            // Arrange
            var dummyUsers = new List<UserDto>
            {
                new UserDto { UserId = Guid.NewGuid().ToString(), Username = "User1", Email = "user1@example.com", FirstName = "First1", LastName = "Last1", PhoneNumber = "1234567890", Role = "Role1" },
                new UserDto { UserId = Guid.NewGuid().ToString(), Username = "User2", Email = "user2@example.com", FirstName = "First2", LastName = "Last2", PhoneNumber = "0987654321", Role = "Role2" }
                // Add more users as needed
            };
            _userRepository.GetAllAsync().Returns(Task.FromResult(Result<List<UserDto>>.Success(dummyUsers)));

            // Act
            var result = await _handler.Handle(new GetAllUserQuery(), new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Users);
            Assert.Equal(dummyUsers.Count, result.Users.Count);
            foreach (var user in dummyUsers)
            {
                var userDto = result.Users.FirstOrDefault(u => u.UserId == user.UserId);
                Assert.NotNull(userDto);
                Assert.Equal(user.Username, userDto.Username);
                Assert.Equal(user.Email, userDto.Email);
                Assert.Equal(user.FirstName, userDto.FirstName);
                Assert.Equal(user.LastName, userDto.LastName);
                Assert.Equal(user.PhoneNumber, userDto.PhoneNumber);
                Assert.Equal(user.Role, userDto.Role);
            }
        }
    }
}
