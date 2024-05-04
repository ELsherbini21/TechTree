using TechTree.DAL.Models;

namespace TechTree.PersantionLayer.ViewModels
{
    public class ContentViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string HtmlContent { get; set; }

        public string VideoLink { get; set; }


        public int CategoryItemId { get; set; }

        public int? CategoryId { get; set; }
        //public virtual CategoryItem? CategoryItem { get; set; } 


    }
}
