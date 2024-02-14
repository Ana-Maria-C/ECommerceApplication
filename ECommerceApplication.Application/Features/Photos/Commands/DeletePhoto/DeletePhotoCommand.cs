using MediatR;

namespace ECommerceApplication.Application.Features.Photos.Commands.DeletePhoto
{
    public class DeletePhotoCommand : IRequest<DeletePhotoCommandResponse>
    {
        public Guid PhotoId { get; set; }
    }
}