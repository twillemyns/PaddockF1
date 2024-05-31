using Microsoft.EntityFrameworkCore;
using PaddockF1.Module.Forum.Models;

namespace PaddockF1.Module.Forum.Data;

public class ForumContext(DbContextOptions<ForumContext> options) : DbContext(options)
{
    public DbSet<Topic> Topics { get; set; } = default!;
    
    public DbSet<Message> Messages { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.HasMany(e => e.Messages).WithOne(e => e.Topic).HasForeignKey(e => e.TopicId);
            entity.ToTable("Topics");
        });
        
        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.HasOne(e => e.Topic).WithMany(e => e.Messages).HasForeignKey(e => e.TopicId);
        });
    }
}