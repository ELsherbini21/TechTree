using System.ComponentModel.DataAnnotations;

namespace TechTree.PersantionLayer.ViewModels
{
    public class MediaTypeViewModel
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        [Display(Name ="ImagePath ")]
        public string ?thumbnailImagePath { get; set; }



        [Display(Name = "File")]
        public IFormFile? UploadImage { get; set; }
    }
}
