using System.ComponentModel.DataAnnotations;

namespace TechTree.PersantionLayer.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }


        [Required (ErrorMessage ="Invalid User Name ")]
        [MinLength(4)]
        [MaxLength(25)]
        public string UserName { get; set; }



        [Required]
        [MinLength(4)]
        [MaxLength(15)] 
        public string FirstName { get; set; }


        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string LastName { get; set; }

        public bool IsSelected { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        //public ICollection<string> Roles { get; set; }
    }
}
