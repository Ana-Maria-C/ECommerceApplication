using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.Users.Queries.GetByEmailUser
{
    public class GetByEmailUserResponse : BaseResponse
    {
        public GetByEmailUserResponse() : base()
        {

        }
        public UserDto User { get; set; }
    }
}