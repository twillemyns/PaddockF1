using PaddockF1.Module.Forum.Models;

namespace PaddockF1.Module.Authentication.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public IEnumerable<Topic> Topics { get; set; } = new List<Topic>();
    
    public IEnumerable<Message> Messages { get; set; } = new List<Message>();
}