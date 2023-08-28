using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DAL.Configurations;

public class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategory>
{
    public void Configure(EntityTypeBuilder<BlogCategory> builder)
    {
        builder.HasOne(bc => bc.Blog)
            .WithMany(b => b.BlogCategories)
            .HasForeignKey(bc => bc.BlogId);
        builder.HasOne(bc => bc.Category)
            .WithMany(c => c.BlogCategories)
            .HasForeignKey(bc => bc.CategoryId);
        builder.Ignore(b => b.IsDeleted);
    }
}
