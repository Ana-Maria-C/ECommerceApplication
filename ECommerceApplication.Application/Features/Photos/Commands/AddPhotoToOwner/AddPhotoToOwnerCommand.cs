using MediatR;
using Microsoft.AspNetCore.Http;

namespace ECommerceApplication.Application.Features.Photos.Commands.AddPhotoToOwner
{
    public class AddPhotoToOwnerCommand : IRequest<AddPhotoToOwnerCommandResponse>
    {
        public Guid OwnerId { get; set; }
        public IFormFile Photo { get; set; }
    }
}
