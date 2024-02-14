using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.Products.Queries.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, GetAllProductQueryResponse>
    {
        private readonly IProductRepository repository;

        public GetAllProductQueryHandler(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            GetAllProductQueryResponse response = new();
            var result = await repository.GetAllAsync();
            response.Products = result.Value.Select(e => new ProductDto
            {
                ProductId = e.ProductId,
                CompanyId = e.CompanyId,
                ProductName = e.ProductName,
                Price = e.Price,
                Description = e.Description,
                ImageUrl = e.ImageUrl,
                Category = new CategoryDto
                {
                    CategoryId = e.Category.CategoryId,
                    CategoryName = e.Category.CategoryName
                }
            }).ToList();
            return response;
        }
    }
}
