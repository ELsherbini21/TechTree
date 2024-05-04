using System.ComponentModel.DataAnnotations;

namespace TechTree.PersantionLayer.ViewModels
{
    public class RoleFormViewModel
    {
        [Required]
        public string Name { get; set; }

        public string? Id { get; set; }
    }


}
