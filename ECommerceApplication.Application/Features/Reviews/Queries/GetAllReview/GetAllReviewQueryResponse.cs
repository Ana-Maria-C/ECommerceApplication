using ECommerceApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Reviews.Queries.GetAllReview
{
    public class GetAllReviewQueryResponse:BaseResponse
    {
        public GetAllReviewQueryResponse():base()
        {

        }
        public List<ReviewDto> Reviews { get; set; }
    }
}
