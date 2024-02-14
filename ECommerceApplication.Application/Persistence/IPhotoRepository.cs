using ECommerceApplication.Domain.Entities;

namespace ECommerceApplication.Application.Persistence
{
    public interface IPhotoRepository : IAsyncRepository<Photo>
    {
        Task<List<Photo>> GetByTaskItemIdAsync(Guid taskItemId);
    }
}