using TechTree.DAL.Models;

namespace TechTree.BLL.Interfaces
{
    public interface IUserCategoryRepository : IGenericRepository<UserCategory>
    {
        Task RemoveRange(List<UserCategory> userCategories);

        Task AddRange(List<UserCategory> userCategories);
    }

}
