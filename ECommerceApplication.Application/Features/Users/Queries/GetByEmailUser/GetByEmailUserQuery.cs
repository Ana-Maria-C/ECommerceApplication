using MediatR;

namespace ECommerceApplication.Application.Features.Users.Queries.GetByEmailUser
{
    public class GetByEmailUserQuery: IRequest<GetByEmailUserResponse>
    {
        public string Email { get; set;}
    }
}
