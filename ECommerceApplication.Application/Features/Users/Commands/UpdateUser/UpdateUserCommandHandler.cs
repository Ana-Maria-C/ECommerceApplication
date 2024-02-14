using ECommerceApplication.Application.Features.Users.Queries;
using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommandResponse>
    {
        private readonly IUserManager repository;

        public UpdateUserCommandHandler(IUserManager repository)
        {
            this.repository = repository;
        }

        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // Validate the request
            var validator = new UpdateUserCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new UpdateUserCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            // Retrieve the user from the repository
            var user = await repository.FindByIdAsync(request.Id);

            if (!user.IsSuccess)
            {
                return new UpdateUserCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { $"User with {request.Id} not found" }
                };
            }

            request.Username ??= user.Value.Username;
            request.Email ??= user.Value.Email;
            request.FirstName ??= user.Value.FirstName;
            request.LastName ??= user.Value.LastName;
            request.PhoneNumber ??= user.Value.PhoneNumber;
            request.Role ??= user.Value.Role;

            // Update user profile

            UserDto userDto = new()
            {
                UserId = user.Value.UserId,
                Username = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Role = request.Role

            };

            // Save the updated user
            var result = await repository.UpdateAsync(userDto);

            if(!result.IsSuccess)
            {
                // Handle failure result
                return new UpdateUserCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }

            return new UpdateUserCommandResponse
            {
                Success = true,
                User = new UpdateUserDto
                {
                    Username = result.Value.Username,
                    Email = result.Value.Email,
                    FirstName = result.Value.FirstName,
                    LastName = result.Value.LastName,
                    PhoneNumber = result.Value.PhoneNumber,
                    Role = result.Value.Role
                }
            };
        }
    }
}