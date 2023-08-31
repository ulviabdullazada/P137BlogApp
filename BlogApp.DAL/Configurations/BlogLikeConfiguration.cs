using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DAL.Configurations;

public class BlogLikeConfiguration : IEntityTypeConfiguration<BlogLike>
{
    public void Configure(EntityTypeBuilder<BlogLike> builder)
    {
        builder.HasOne(bl => bl.Blog)
            .WithMany(b => b.BlogLikes)
            .HasForeignKey(bl => bl.BlogId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(bl => bl.AppUser)
            .WithMany(u => u.BlogLikes)
            .HasForeignKey(bl => bl.AppUserId);
        builder.Property(b => b.Reaction)
            .IsRequired();
        builder.Ignore(b => b.IsDeleted);
    }
}
