using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
    {
        private readonly ICategoryRepository repository;

        public UpdateCategoryCommandHandler(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            // Validate the request
            var validator = new UpdateCategoryCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new UpdateCategoryCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            // Retrieve the category from the repository
            var category = await repository.FindByIdAsync(request.CategoryId);

            if (category == null)
            {
                return new UpdateCategoryCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Category not found." }
                };
            }

            // Update category profile
            var updateResult = category.Value.Update(request.CategoryName);

            if (updateResult.IsSuccess)
            {
                // Save the updated category
                await repository.UpdateAsync(updateResult.Value);

                return new UpdateCategoryCommandResponse
                {
                    Success = true,
                    Category = new UpdateCategoryDto
                    {
                        CategoryId = updateResult.Value.CategoryId,
                        CategoryName = updateResult.Value.CategoryName,
                    }
                };
            }
            else
            {
                // Handle failure result
                return new UpdateCategoryCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { updateResult.Error }
                };
            }
        }
    }
}