using MediatR;
using ECommerceApplication.Application.Persistence;

namespace ECommerceApplication.Application.Features.Products.Queries.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, GetByIdProductQueryResponse>
    {
        private readonly IProductRepository repository;

        public GetByIdProductQueryHandler(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            var @event = await repository.FindByIdAsync(request.ProductId);
            if (!@event.IsSuccess)
            {
                return new GetByIdProductQueryResponse
                {
                    Success = false,
                    ValidationsErrors = [@event.Error]
                };
            }

            return new GetByIdProductQueryResponse
            {
                Success = true,
                Product = new ProductDto
                {
                    ProductId = @event.Value.ProductId,
                    CompanyId = @event.Value.CompanyId,
                    ProductName = @event.Value.ProductName,
                    Price = @event.Value.Price,
                    Description = @event.Value.Description,
                    ImageUrl = @event.Value.ImageUrl,
                    Category = new CategoryDto
                    {
                        CategoryId = @event.Value.Category.CategoryId,
                        CategoryName = @event.Value.Category.CategoryName
                    }
                }
            };
        }
    }
}
