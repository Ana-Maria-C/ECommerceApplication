
using ECommerceApplication.Application.Features.Products.Queries.GetAllProduct;
using NSubstitute;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Entities;

using ECommerceApplication.Domain.Common;

namespace ECommercerApplication.Application.Tests.Queries.ProductTests
{
    public class GetAllProductQueryHandlerTests
    {
        private readonly IProductRepository _repository;
        private readonly GetAllProductQueryHandler _handler;

        public GetAllProductQueryHandlerTests()
        {
            _repository = Substitute.For<IProductRepository>();
            _handler = new GetAllProductQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_ReturnAllProducts()
        {
            var companyId = Guid.NewGuid();
            var product = new List<Product>
            {
                Product.Create(companyId, "Product 1",10).Value,
                Product.Create(companyId, "Product 2", 2).Value
            };

            var result = Result<IReadOnlyList<Product>>.Success(product);
            _repository.GetAllAsync().Returns(Task
                               .FromResult<Result<IReadOnlyList<Product>>>(result));
            var response = await _handler.Handle(new GetAllProductQuery(), new CancellationToken());
            Assert.NotNull(response);
            Assert.Equal(product.Count, response.Products.Count);
            Assert.All(response.Products, productDto =>
                           Assert.Contains(product, c => c.ProductId == productDto.ProductId && c.ProductName == productDto.ProductName && c.Price == productDto.Price));
        }


    }
}
