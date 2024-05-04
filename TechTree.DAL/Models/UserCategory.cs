namespace TechTree.DAL.Models
{
    public class UserCategory : ModelBase
    {
        

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }



        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


    }
}
