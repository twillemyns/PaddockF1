namespace PaddockF1.Module.Forum.Models;

public class Message
{
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public string Content { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string UserId { get; set; } = default!;
    
    public Guid TopicId { get; set; }
    
    public Topic Topic { get; set; } = default!;
}