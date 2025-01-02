using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Data.Configuration;

public class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Title).IsRequired();
        builder.Property(e => e.Description).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.HasMany(e => e.Messages)
            .WithOne(e => e.Topic)
            .HasForeignKey(m => m.TopicId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(e => e.Author)
            .WithMany(e => e.Topics)
            .HasForeignKey(e => e.AuthorId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }
}