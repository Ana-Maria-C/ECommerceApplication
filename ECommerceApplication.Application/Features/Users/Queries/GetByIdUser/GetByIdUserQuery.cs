using MediatR;

namespace ECommerceApplication.Application.Features.Users.Queries.GetByIdUser
{
    public class GetByIdUserQuery : IRequest<GetByIdUserQueryResponse>
    {
        public string UserId { get; set; }
    }
}
