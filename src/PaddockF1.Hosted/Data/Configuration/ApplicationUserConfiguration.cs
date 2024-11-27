using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Data.Configuration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
            builder.Ignore(m => m.PhoneNumber);
            builder.Ignore(m => m.PhoneNumberConfirmed);
            builder.HasMany(m => m.Topics)
                .WithOne(m => m.Author)
                .HasForeignKey(m => m.AuthorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(m => m.Messages)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
    }
}