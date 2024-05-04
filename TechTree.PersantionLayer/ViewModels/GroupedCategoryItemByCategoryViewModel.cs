namespace TechTree.PersantionLayer.ViewModels
{
    public class GroupedCategoryItemByCategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IGrouping<int, CategoryItemDetailsViewModel> CategoryItems { get; set; }
    }
}
