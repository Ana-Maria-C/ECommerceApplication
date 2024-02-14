using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApplication.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ECommerceApplicationContext context) : base(context)
        {
        }
        public async Task<bool> CategoryExists(Guid categoryId)
        {
            return await context.Categories.AnyAsync(c => c.CategoryId == categoryId);
        }
    }
}