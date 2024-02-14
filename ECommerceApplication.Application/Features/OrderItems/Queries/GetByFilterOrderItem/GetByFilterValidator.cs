using ECommerceApplication.Application.Features.OrderItems.Queries.GetByFilterOrderItem;
using ECommerceApplication.Application.Features.OrderItems.Queries.GetByIdOrderItem;
using FluentValidation;

namespace ECommerceApplication.OrderItems.Features.GetByIdOrderItem.Queries.GetByFilterOrderItem
{
    public class GetByIdOrderItemValidator : AbstractValidator<GetByFilterOrderItemQuery>
    {
        public GetByIdOrderItemValidator()
        {
            RuleFor(p => p.OrderItemId)
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}
