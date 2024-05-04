using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechTree.DAL.Models;

namespace TechTree.DAL.Data.Configurations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(appUser => appUser.FirstName).HasMaxLength(50);

            builder.Property(appUser => appUser.LastName).HasMaxLength(50);

           


            builder.HasMany(appUser => appUser.UserCategories)
                .WithOne(userCategory => userCategory.ApplicationUser)
                .HasForeignKey(userCategory => userCategory.ApplicationUserId);
        }
    }

}
