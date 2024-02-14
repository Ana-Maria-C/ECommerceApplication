using ECommerceApplication.Application.Features.Photos.Commands.DeletePhoto;
using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.Photos.Commands.DeletePhotoCommandHandler
{
    public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, DeletePhotoCommandResponse>
    {
        private readonly IPhotoRepository _photoRepository;
        public DeletePhotoCommandHandler(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public async Task<DeletePhotoCommandResponse> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeletePhotoCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return new DeletePhotoCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }
            var photoToDelete = await _photoRepository.FindByIdAsync(request.PhotoId);
            if (!photoToDelete.IsSuccess)
            {
                return new DeletePhotoCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Photo with the provided ID does not exist." }
                };
            }
            var result = await _photoRepository.DeleteAsync(request.PhotoId);
            if (!result.IsSuccess)
            {
                return new DeletePhotoCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }
            return new DeletePhotoCommandResponse
            {
                Success = true
            };
        }
    }
}