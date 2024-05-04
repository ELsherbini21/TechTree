using System.ComponentModel.DataAnnotations;
using TechTree.DAL.Models;

namespace TechTree.PersantionLayer.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "User Name is Required")]
        public string UserName { get; set; }




        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }




        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }



        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(5, ErrorMessage = "Minimum Password Length is 5")]
        [MaxLength(30, ErrorMessage = "Maximum Password Length is 30")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare(nameof(Password), ErrorMessage = "Confirm Password doesn't match with Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }



    }
}
