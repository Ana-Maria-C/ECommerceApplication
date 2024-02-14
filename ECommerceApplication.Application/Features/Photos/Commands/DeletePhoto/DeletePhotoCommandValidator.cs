using FluentValidation;

namespace ECommerceApplication.Application.Features.Photos.Commands.DeletePhoto
{
    public class DeletePhotoCommandValidator : AbstractValidator<DeletePhotoCommand>
    {
        public DeletePhotoCommandValidator()
        {
            RuleFor(p => p.PhotoId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}