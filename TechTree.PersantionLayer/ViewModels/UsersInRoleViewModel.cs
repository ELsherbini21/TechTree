using System.ComponentModel.DataAnnotations;

namespace TechTree.PersantionLayer.ViewModels
{
    public class UsersInRoleViewModel
    {
        public string AppUserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public bool IsSelected{ get; set; }

    }
}
