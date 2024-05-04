namespace TechTree.DAL.Models
{
    public class CategoryItem : ModelBase
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateTimeItemReleased { get; set; }


        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }


        public int MediaTypeId { get; set; }
        public virtual MediaType MediaType { get; set; }


        public int ContentId{ get; set; }
        public virtual Content Content { get; set; }





    }

}
