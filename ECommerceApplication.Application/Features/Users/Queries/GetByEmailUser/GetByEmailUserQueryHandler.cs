using ECommerceApplication.Application.Persistence;
using MediatR;


namespace ECommerceApplication.Application.Features.Users.Queries.GetByEmailUser
{
    public class GetByEmailUserQueryHandler : IRequestHandler<GetByEmailUserQuery, GetByEmailUserResponse>
    {
        private readonly IUserManager userRepository;
        public GetByEmailUserQueryHandler(IUserManager userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<GetByEmailUserResponse> Handle(GetByEmailUserQuery request, CancellationToken cancellationToken)
        {
            var result= await userRepository.FindByEmailAsync(request.Email);
            if (!result.IsSuccess)
                return new GetByEmailUserResponse { Success = false, Message = result.Error };
            var userDto = result.Value;
            return new GetByEmailUserResponse
            {
                Success = true,
                User = new UserDto
                {
                    UserId = userDto.UserId,
                    Username = userDto.Username,
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    PhoneNumber = userDto.PhoneNumber,
                    Role = userDto.Role,
                }
            };
        }
    }
}
