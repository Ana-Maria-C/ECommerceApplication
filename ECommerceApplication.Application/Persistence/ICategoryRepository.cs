using ECommerceApplication.Domain.Entities;

namespace ECommerceApplication.Application.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<bool> CategoryExists(Guid categoryId);
    }
}
