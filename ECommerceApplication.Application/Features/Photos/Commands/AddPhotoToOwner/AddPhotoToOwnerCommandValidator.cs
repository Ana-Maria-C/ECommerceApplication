using FluentValidation;

namespace ECommerceApplication.Application.Features.Photos.Commands.AddPhotoToOwner
{
    public class AddPhotoToTaskItemCommandValidator : AbstractValidator<AddPhotoToOwnerCommand>
    {
        public AddPhotoToTaskItemCommandValidator()
        {
            RuleFor(p => p.OwnerId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Photo)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}