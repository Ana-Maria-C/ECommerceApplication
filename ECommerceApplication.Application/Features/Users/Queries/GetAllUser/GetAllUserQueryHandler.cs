using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.Users.Queries.GetAllUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, GetAllUserResponse>
    {
        private readonly IUserManager userRepository;

        public GetAllUserQueryHandler(IUserManager userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<GetAllUserResponse> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            GetAllUserResponse response = new();
            var result = await userRepository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Users = result.Value.Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    Role = u.Role,

                }).ToList();
            }
            return response;
        }
    }
}