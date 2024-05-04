namespace TechTree.PersantionLayer.ViewModels
{
    public class UserRoleViewModel
    {
        public string ApplicationUserId { get; set; }
        public string ApplicationUserName { get; set; }
        public List<RoleSelectViewModel>  Roles{ get; set; }
    }
}
