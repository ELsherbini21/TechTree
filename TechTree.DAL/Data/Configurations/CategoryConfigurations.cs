using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTree.DAL.Models;

namespace TechTree.DAL.Data.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.Property(category => category.Id).UseIdentityColumn(10, 10);

            builder.Property(category => category.Title).HasMaxLength(200);


            builder.HasMany(category => category.CategoryItems)
                .WithOne(categoryItem => categoryItem.Category)
                .HasForeignKey(categoryItem => categoryItem.CategoryId);


            builder.HasMany(category => category.UserCategories)
                .WithOne(userCategory => userCategory.Category)
                .HasForeignKey(userCategory => userCategory.CategoryId);



        }
    }

}
