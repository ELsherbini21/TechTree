using TechTree.BLL.Interfaces;
using TechTree.DAL.Data;
using TechTree.DAL.Models;

namespace TechTree.BLL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
