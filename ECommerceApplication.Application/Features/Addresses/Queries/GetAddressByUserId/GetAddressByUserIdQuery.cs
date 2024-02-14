using MediatR;

namespace ECommerceApplication.Application.Features.Addresses.Queries.GetAddressByUserId
{
    public class GetAddressByUserIdQuery : IRequest<GetAddressByUserIdResponse>
    {
        public Guid UserId { get; set; } = default!;
    }
}