using ECommerceApplication.App.ViewModels;

namespace ECommerceApplication.App.Services
{
	public class ApiResponseForProduct
	{ 
		public ProductViewModel Product { get; set; }
		public bool Success { get; set; }
		public string Message { get; set; }
		public List<String> ValidationsErrors { get; set; }
		
	}
}
