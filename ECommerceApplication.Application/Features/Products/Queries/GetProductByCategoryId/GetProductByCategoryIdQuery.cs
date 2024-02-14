using MediatR;

namespace ECommerceApplication.Application.Features.Products.Queries.GetProductByCategoryId
{
    public class GetProductByCategoryIdQuery : IRequest<GetProductByCategoryIdResponse>
    {
        public Guid CategoryId { get; set; } = default!;
    }
}