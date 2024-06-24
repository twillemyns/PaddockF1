using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Data;


/// <summary>
/// Contexte de base de données de l'application
/// </summary>
/// <param name="options">Paramètres du contexte</param>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    /// <summary>
    /// Dépôt des sujets de discussion
    /// </summary>
    public DbSet<Topic> Topics { get; set; } = default!;

    /// <summary>
    /// Dépôt des messages
    /// </summary>
    public DbSet<Message> Messages { get; set; } = default!;

    /// <summary>
    /// Modèle de création de la base de données
    /// </summary>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Ignore(m => m.PhoneNumber);
            entity.Ignore(m => m.PhoneNumberConfirmed);
            entity.HasMany(m => m.Topics)
                .WithOne(m => m.Author)
                .HasForeignKey(m => m.AuthorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(m => m.Messages)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
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
                .HasForeignKey(m => m.TopicId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Author)
                .WithMany(e => e.Topics)
                .HasForeignKey(e => e.AuthorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.HasOne(e => e.Topic)
                .WithMany(e => e.Messages)
                .HasForeignKey(e => e.TopicId)
                .IsRequired();
            entity.HasOne(e => e.User)
                .WithMany(e => e.Messages)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}