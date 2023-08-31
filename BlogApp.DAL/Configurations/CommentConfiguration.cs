using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DAL.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(c => c.Text)
            .IsRequired();
        builder.Property(c => c.CreatedDate)
            .HasDefaultValueSql("getutcdate()");
        builder.HasOne(c => c.Blog)
            .WithMany(b => b.Comments)
            .HasForeignKey(c => c.BlogId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(c => c.AppUser)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.AppUserId);
        builder.HasOne(c=>c.Parent)
            .WithMany(p=>p.Children)
            .HasForeignKey(c => c.ParentId);
    }
}
