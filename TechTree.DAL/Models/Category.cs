using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TechTree.DAL.Models
{
    public class Category : ModelBase
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string thumbnailImagePath { get; set; }


        public virtual ICollection<CategoryItem> CategoryItems { get; set; } = new List<CategoryItem>();


        public virtual ICollection<UserCategory> UserCategories { get; set; } = new List<UserCategory>();



    }
}
