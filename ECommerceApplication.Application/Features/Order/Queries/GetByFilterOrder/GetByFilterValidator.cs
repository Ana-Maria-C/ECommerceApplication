using ECommerceApplication.Application.Features.Order.Queries.GetByFilterOrder;
using ECommerceApplication.Application.Features.OrderItems.Queries.GetByIdOrderItem;
using FluentValidation;

namespace ECommerceApplication.OrderItems.Features.Order.Queries.GetByFilterOrder
{
    public class GetByIdOrderItemValidator : AbstractValidator<GetByFilterOrderQuery>
    {
        public GetByIdOrderItemValidator()
        {
            RuleFor(p => p.UserId)
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}
