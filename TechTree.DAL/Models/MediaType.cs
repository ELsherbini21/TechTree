namespace TechTree.DAL.Models
{
    public class MediaType : ModelBase
    {
        public string Title { get; set; }

        public string thumbnailImagePath { get; set; }


        public virtual ICollection<CategoryItem> CategoryItems { get; set; } = new List<CategoryItem>();
    }
}
