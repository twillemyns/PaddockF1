using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
        public DbSet<Topic> Topics { get; set; } = default!;
        
        public DbSet<Message> Messages { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Ignore(m => m.PhoneNumber);
            entity.Ignore(m => m.PhoneNumberConfirmed);
            entity.HasMany(m => m.Topics)
                .WithOne(m => m.Author)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(m => m.Messages)
                .WithOne(m => m.User)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.HasMany(e => e.Messages)
                .WithOne(e => e.Topic)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.HasOne(e => e.Topic)
                .WithMany(e => e.Messages);
        });
    }
}