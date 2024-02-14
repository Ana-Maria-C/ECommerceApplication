using ECommerceApplication.Application.Features.Products.Queries;
using ECommerceApplication.Application.Features.Products.Queries.GetProductByCategoryId;
using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.Products.Queries.GetProductByCategoryId
{
    public class GetProductByCategoryIdQueryHandler : IRequestHandler<GetProductByCategoryIdQuery, GetProductByCategoryIdResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public GetProductByCategoryIdQueryHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<GetProductByCategoryIdResponse> Handle(GetProductByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var productResult = await _productRepository.GetProductByCategoryIdAsync(request.CategoryId);

            if (!productResult.IsSuccess)
            {
                return new GetProductByCategoryIdResponse
                {
                    Success = false,
                    Products = null
                };
            }

            var products = productResult.Value;

            var category = await _categoryRepository.FindByIdAsync(productResult.Value[0].CategoryId);

            var productDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                productDtos.Add(new ProductDto
                {
                    ProductId = product.ProductId,
                    CompanyId = product.CompanyId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Category = new CategoryDto()
                    {
                        CategoryId = product.Category.CategoryId,
                        CategoryName = category.Value.CategoryName
                    }

                });
            }

            return new GetProductByCategoryIdResponse
            {
                Success = true,
                Products = productDtos
            };
        }
    }
}