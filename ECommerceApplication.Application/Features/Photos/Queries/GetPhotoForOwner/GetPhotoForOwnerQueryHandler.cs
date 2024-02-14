using ECommerceApplication.Application.Features.Photos.Queries;
using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.Photos.Queries.GetPhotoForOwner
{
    public class GetPhotoForOwnerQueryHandler : IRequestHandler<GetPhotoForOwnerQuery, GetPhotoForOwnerQueryResponse>
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserManager _userManager;
        public GetPhotoForOwnerQueryHandler(IPhotoRepository photoRepository, IProductRepository productRepository, IUserManager userManager)
        {
           _photoRepository = photoRepository;
           _productRepository = productRepository;
           _userManager = userManager;
        }

        public async Task<GetPhotoForOwnerQueryResponse> Handle(GetPhotoForOwnerQuery request, CancellationToken cancellationToken)
        {
            bool productBool = true;
            var product = await _productRepository.FindByIdAsync(request.OwnerId);
            if (!product.IsSuccess)
            {
                productBool = false;
            }

            bool userBool = true;
            var user = await _userManager.FindByIdAsync(request.OwnerId);
            if (!user.IsSuccess)
            {
                userBool = false;
            }

            if (!user.IsSuccess && !product.IsSuccess)
            {
                return new GetPhotoForOwnerQueryResponse()
                {
                    Success = false,
                    ValidationsErrors = new List<string> { $"Entity with id {request.OwnerId} does not exist." }
                };
            }
            var photos = await _photoRepository.GetByTaskItemIdAsync(request.OwnerId);
            var photoDtos = photos.Select(p => new PhotoDto
            {
                PhotoId = p.PhotoId,
                OwnerId = p.OwnerId,
                ImageData = Convert.ToBase64String(p.ImageData)
            }).ToList();
            return new GetPhotoForOwnerQueryResponse
            {
                Success = true,
                Photos = photoDtos
            };
        }
    }
}