using TechTree.BLL.Interfaces;
using TechTree.DAL.Data;
using TechTree.DAL.Models;

namespace TechTree.BLL.Repositories
{
    public class CategoryItemRepository : GenericRepository<CategoryItem>, ICategoryItemRepository
    {
        public CategoryItemRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
      
    }
}
