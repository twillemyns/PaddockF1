namespace PaddockF1.Hosted.Data.Models;

/// <summary>
/// Classe représentant un message posté par un utilisateur dans le forum
/// </summary>
public class Message
{
    /// <summary>
    /// ID de l'entité
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();
    
    /// <summary>
    /// Contenu du message
    /// </summary>
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// Date et heure de création du message
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// ID de l'utilisateur ayant posté le message
    /// </summary>
    public string UserId { get; set; } = default!;

    /// <summary>
    /// Utilisateur ayant posté le message
    /// </summary>
    public ApplicationUser User { get; set; } = default!;
    
    /// <summary>
    /// ID du sujet de discussion auquel le message est rattaché
    /// </summary>
    public Guid TopicId { get; set; }
    
    /// <summary>
    /// Sujet de discussion auquel le message est rattaché
    /// </summary>
    public Topic Topic { get; set; } = default!;
}