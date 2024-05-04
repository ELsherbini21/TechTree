using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TechTree.DAL.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TechTree.PersantionLayer.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is Required Sir ")]
        [StringLength(20, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        [Display(Name = "File")]
        public IFormFile? thumbnailImagePathUploadImage { get; set; }


        public string? thumbnailImagePath { get; set; }




        //public virtual ICollection<CategoryItem> CategoryItems { get; set; } = new List<CategoryItem>();


        //public virtual ICollection<UserCategory> UserCategories { get; set; } = new List<UserCategory>();



    }
}
