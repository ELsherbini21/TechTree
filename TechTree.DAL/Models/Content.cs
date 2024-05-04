namespace TechTree.DAL.Models
{
    public class Content : ModelBase
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string HtmlContent { get; set; }

        public string VideoLink { get; set; }


        public int CategoryItemId { get; set; }
        public virtual CategoryItem? CategoryItem { get; set; }


    }
}
