using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Api.IntegrationTests.Controllers
{
    public class ApiResponse<T>
    {
        public T? Products { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? ValidationErrors { get; set; }
    }
}
