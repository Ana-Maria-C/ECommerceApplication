using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Entities;
using ECommerceApplication.Infrastructure;
using ECommerceApplication.Infrastructure.Repositories;

namespace ECommerceApplication.Infrastructure.Repositories
{
    public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(ECommerceApplicationContext context) : base(context)
        {

        }

        public Task<List<Photo>> GetByTaskItemIdAsync(Guid ownerId)
        {
            return Task.FromResult(context.Photos.Where(p => p.OwnerId == ownerId).ToList());
        }
    }
}