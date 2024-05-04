using System.ComponentModel.DataAnnotations;
using TechTree.DAL.Models;

namespace TechTree.PersantionLayer.ViewModels
{
    public class CategoryItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is Required Sir ")]
        [StringLength(20, MinimumLength = 5)]
        public string Title { get; set; }

        [Required(ErrorMessage = "This Field is Required Sir ")]
        [MinLength(5)]
        public string Description { get; set; }

        //[DisplayFormat(DataFormatString = "dd-MM-yyyy hh:mm")]
        [Required(ErrorMessage = "This Field is Required Sir ")]
        [Display(Name = "Release DateTime")]
        public DateTime DateTimeItemReleased { get; set; } = DateTime.Now;



        public int CategoryId { get; set; }
        //public virtual Category Category { get; set; }



        public int MediaTypeId { get; set; }
        //public virtual MediaType MediaType { get; set; }


        public virtual Content? Content { get; set; }





    }
}
