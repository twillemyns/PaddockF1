namespace PaddockF1.Hosted.Data.Models;

public class Topic
{

    public Guid Id { get; init; } = Guid.NewGuid();
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string AuthorId { get; set; } = string.Empty;

    public ApplicationUser Author { get; set; } = default!;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public IEnumerable<Message> Messages { get; set; } = new List<Message>();
}