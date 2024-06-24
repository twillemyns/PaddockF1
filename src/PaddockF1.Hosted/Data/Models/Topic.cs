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
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Texte introductif du sujet de discussion
    /// </summary>
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