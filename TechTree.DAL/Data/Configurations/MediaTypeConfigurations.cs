using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechTree.DAL.Models;

namespace TechTree.DAL.Data.Configurations
{
    public class MediaTypeConfigurations : IEntityTypeConfiguration<MediaType>
    {
        public void Configure(EntityTypeBuilder<MediaType> builder)
        {
            builder.ToTable("MediaTypes");

            builder.Property(mediaType => mediaType.Id).UseIdentityColumn(10, 10);

            builder.Property(mediaType => mediaType.Title).HasMaxLength(100);

            builder.HasMany(mediaType => mediaType.CategoryItems)
                .WithOne(categoryItem => categoryItem.MediaType)
                .HasForeignKey(categoryItem => categoryItem.MediaTypeId);




        }
    }

}
