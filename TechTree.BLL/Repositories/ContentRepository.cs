using TechTree.BLL.Interfaces;
using TechTree.DAL.Data;
using TechTree.DAL.Models;

namespace TechTree.BLL.Repositories
{
    public class ContentRepository : GenericRepository<Content>, IContentRepository
    {
        public ContentRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
