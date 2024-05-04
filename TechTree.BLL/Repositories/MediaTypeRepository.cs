using TechTree.BLL.Interfaces;
using TechTree.DAL.Data;
using TechTree.DAL.Models;

namespace TechTree.BLL.Repositories
{
    public class MediaTypeRepository : GenericRepository<MediaType>, IMediaTypeRepository
    {
        public MediaTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
