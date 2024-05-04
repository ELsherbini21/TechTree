using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechTree.DAL.Models;

namespace TechTree.DAL.Data.Configurations
{
    public class ContentConfigurations : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.ToTable("Contents");

            builder.Property(content => content.Id).UseIdentityColumn(10, 10);

            builder.Property(mediaType => mediaType.Title).HasMaxLength(100);

            builder.HasOne(content => content.CategoryItem)
                .WithOne(categoryItem => categoryItem.Content)
                .HasForeignKey<Content>(content => content.CategoryItemId);





        }
    }

}
