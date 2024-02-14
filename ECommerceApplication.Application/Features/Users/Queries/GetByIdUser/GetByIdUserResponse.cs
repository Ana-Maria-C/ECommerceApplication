using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.Users.Queries.GetByIdUser
{
    public class GetByIdUserQueryResponse : BaseResponse
    {
        public GetByIdUserQueryResponse() : base()
        {

        }
        public UserDto User { get; set; }
    }
}