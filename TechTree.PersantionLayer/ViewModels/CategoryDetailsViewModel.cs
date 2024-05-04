namespace TechTree.PersantionLayer.ViewModels
{
    public class CategoryDetailsViewModel
    {
        public IEnumerable<GroupedCategoryItemByCategoryViewModel> GroupedCategoryItemByCategoryViewModels { get; set; }

        public IEnumerable<CategoryViewModel>  Categories { get; set; }

    }
}
