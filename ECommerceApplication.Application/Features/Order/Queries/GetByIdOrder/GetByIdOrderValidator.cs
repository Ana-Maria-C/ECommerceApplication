using ECommerceApplication.Application.Features.Order.Queries.GetByIdOrder;
using ECommerceApplication.Application.Features.OrderItems.Queries.GetByIdOrderItem;
using FluentValidation;

namespace ECommerceApplication.OrderItems.Features.GetByIdOrder.Queries.GetByIdOrder
{
    public class GetByIdOrderValidator : AbstractValidator<GetByIdOrderQuery>
    {
        public GetByIdOrderValidator()
        {
            RuleFor(p => p.OrderId)
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}
