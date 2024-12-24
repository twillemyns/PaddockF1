using System.ComponentModel.DataAnnotations;

namespace PaddockF1.Hosted.Data.Models;

public class Topic
{

    public Guid Id { get; init; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "Le titre est obligatoire.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Le titre doit contenir entre 3 et 50 caractères.")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La description est obligatoire.")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "La description doit contenir entre 5 et 500 caractères.")]
    public string Description { get; set; } = string.Empty;
    
    public string AuthorId { get; set; } = string.Empty;

    public ApplicationUser Author { get; set; } = default!;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public IEnumerable<Message> Messages { get; set; } = new List<Message>();
}