namespace TechTree.PersantionLayer.ViewModels
{
    public class UserCategoryListViewModel
    {
        public int CategoryId { get; set; }

        public ICollection<ApplicationUserViewModel> Users { get; set; }

        public ICollection<ApplicationUserViewModel> UsersSelected { get; set; }
    }
}
