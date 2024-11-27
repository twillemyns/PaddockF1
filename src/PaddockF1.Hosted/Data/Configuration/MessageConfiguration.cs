using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Data.Configuration;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Content).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.HasOne(e => e.Topic)
            .WithMany(e => e.Messages)
            .HasForeignKey(e => e.TopicId)
            .IsRequired();
        builder.HasOne(e => e.User)
            .WithMany(e => e.Messages)
            .HasForeignKey(e => e.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }
}