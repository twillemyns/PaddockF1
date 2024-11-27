using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaddockF1.Hosted.Data.Configuration;
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

        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new TopicConfiguration());
        builder.ApplyConfiguration(new MessageConfiguration());
    }
}