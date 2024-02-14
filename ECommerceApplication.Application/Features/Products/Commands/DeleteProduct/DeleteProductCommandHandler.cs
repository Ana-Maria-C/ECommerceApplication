using ECommerceApplication.Application.Features.Products.Commands.DeleteProduct;
using ECommerceApplication.Application.Persistence;
using MediatR;

namespace GlobalBuyTicket.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductCommandResponse>
    {
        private readonly IProductRepository repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteAsync(request.ProductId);
            if (!result.IsSuccess)
            {
                return new DeleteProductCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }
            return new DeleteProductCommandResponse
            {
                Success = true
            };
        }
    }
}
