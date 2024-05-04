using TechTree.BLL.Interfaces;
using TechTree.DAL.Data;
using TechTree.DAL.Models;

namespace TechTree.BLL.Repositories
{
    public class UserCategoryRepository : GenericRepository<UserCategory>, IUserCategoryRepository
    {
        public UserCategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task RemoveRange(List<UserCategory> userCategories) => _dbContext.RemoveRange(userCategories);

        public async Task AddRange(List<UserCategory> userCategories) => await _dbContext.AddRangeAsync(userCategories);

    }
}
