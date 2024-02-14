using ECommerceApplication.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ECommerceApplication.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }
    }
}
