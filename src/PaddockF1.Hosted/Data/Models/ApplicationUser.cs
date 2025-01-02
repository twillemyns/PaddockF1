using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PaddockF1.Hosted.Data.Models;

/// <summary>
/// Classe représentant un utilisateur de l'application
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Liste des sujets de discussion créés par l'utilisateur
    /// </summary>
    public IEnumerable<Topic> Topics { get; set; } = new List<Topic>();
    
    /// <summary>
    /// Liste des messages postés par l'utilisateur
    /// </summary>
    public IEnumerable<Message> Messages { get; set; } = new List<Message>();

    [NotMapped]
    public IEnumerable<string> Roles { get; set; } = new List<string>();
}