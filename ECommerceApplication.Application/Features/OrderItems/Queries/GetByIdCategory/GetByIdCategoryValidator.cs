using ECommerceApplication.Application.Features.OrderItems.Queries.GetByIdOrderItem;
using FluentValidation;

namespace ECommerceApplication.OrderItems.Features.GetByIdOrderItem.Queries.GetByIdOrderItem
{
    public class GetByIdOrderItemValidator : AbstractValidator<GetByIdOrderItemQuery>
    {
        public GetByIdOrderItemValidator()
        {
            RuleFor(p => p.OrderItemId)
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}
