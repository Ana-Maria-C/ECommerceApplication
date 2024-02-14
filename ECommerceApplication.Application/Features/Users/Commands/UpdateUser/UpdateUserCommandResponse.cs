using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandResponse : BaseResponse
    {
        public UpdateUserCommandResponse() : base()
        {
        }

        public UpdateUserDto User { get; set; }
    }
}