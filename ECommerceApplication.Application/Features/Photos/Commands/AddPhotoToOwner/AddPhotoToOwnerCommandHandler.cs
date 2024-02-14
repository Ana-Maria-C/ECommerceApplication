using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ECommerceApplication.Application.Features.Photos.Commands.AddPhotoToOwner
{
    public class AddPhotoToOwnerCommandHandler : IRequestHandler<AddPhotoToOwnerCommand, AddPhotoToOwnerCommandResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IUserManager _userManager;

        public AddPhotoToOwnerCommandHandler(IProductRepository productRepository, IPhotoRepository photoRepository, IUserManager userManager)
        {
            _productRepository = productRepository;
            _photoRepository = photoRepository;
            _userManager = userManager;
        }

        public async Task<AddPhotoToOwnerCommandResponse> Handle(AddPhotoToOwnerCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddPhotoToTaskItemCommandValidator();
            var validatorResult = validator.Validate(request);
            if (!validatorResult.IsValid)
            {
                return new AddPhotoToOwnerCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
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
                return new AddPhotoToOwnerCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { $"Entity with id {request.OwnerId} not found." }
                };
            }

            var image = await ConvertFormFileToByteArray(request.Photo);
            //var base64Image = Convert.ToBase64String(image);
            var photo = Photo.Create(request.OwnerId, image);
            if (!photo.IsSuccess)
            {
                return new AddPhotoToOwnerCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { photo.Error }
                };
            }
            await _photoRepository.AddAsync(photo.Value);
            return new AddPhotoToOwnerCommandResponse
            {
                Success = true
            };
        }
        public static async Task<byte[]> ConvertFormFileToByteArray(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

    }

}