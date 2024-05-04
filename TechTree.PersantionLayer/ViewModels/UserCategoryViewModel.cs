using Microsoft.AspNetCore.Authentication.Cookies;
using System.Configuration;
using System.Security.Principal;
using TechTree.DAL.Models;

namespace TechTree.PersantionLayer.ViewModels
{
    public class UserCategoryViewModel
    {
        public int Id { get; set; }


        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }


        public bool IsSelected { get; set; }

      

    }


    public class CategoriesToUserViewModel
    {
        public string ApplicationUserId { get; set; }

        public List<Category> Categories { get; set; }

        public List<Category> CategoriesSelected { get; set; }
    }

    public class CoursesToUserViewModel
    {
        public int CategoryId { get; set; }

        public string CategoryTitle { get; set; }

        public bool IsSelected { get; set; }


    }
}
