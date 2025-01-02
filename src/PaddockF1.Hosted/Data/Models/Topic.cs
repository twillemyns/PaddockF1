using System.ComponentModel.DataAnnotations;

namespace PaddockF1.Hosted.Data.Models;

/// <summary>
/// Classe représentant un sujet de discussion dans le forum
/// </summary>
public class Topic
{
    /// <summary>
    /// ID de l'entité
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Titre du sujet de discussion
    /// </summary>
    [Required(ErrorMessage = "Le titre est obligatoire.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Le titre doit contenir entre 3 et 50 caractères.")]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Texte introductif du sujet de discussion
    /// </summary>
    [Required(ErrorMessage = "La description est obligatoire.")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "La description doit contenir entre 5 et 500 caractères.")]
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// ID de l'utilisateur ayant créé le sujet de discussion
    /// </summary>
    public string AuthorId { get; set; } = string.Empty;

    /// <summary>
    /// Utilisateur ayant créé le sujet de discussion
    /// </summary>
    public ApplicationUser Author { get; set; } = default!;
    
    /// <summary>
    /// Date et heure de création du sujet de discussion
    /// </summary> 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Enumérable des messages postés dans le sujet de discussion
    /// </summary>
    public IEnumerable<Message> Messages { get; set; } = new List<Message>();
}