using FluentValidation;

namespace ECommerceApplication.Application.Features.Categories.Queries.DeleteCategory
{
    public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryQuery>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
