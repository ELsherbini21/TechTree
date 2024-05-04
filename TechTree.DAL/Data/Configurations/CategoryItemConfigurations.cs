using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechTree.DAL.Models;

namespace TechTree.DAL.Data.Configurations
{
    public class CategoryItemConfigurations : IEntityTypeConfiguration<CategoryItem>
    {
        public void Configure(EntityTypeBuilder<CategoryItem> builder)
        {
            builder.ToTable("CategoryItems");

            builder.Property(categoryItem => categoryItem.Id).UseIdentityColumn(10, 10);

            builder.Property(categoryItem => categoryItem.Title).HasMaxLength(100);

            builder.Ignore(catItem => catItem.ContentId);







        }
    }

}
