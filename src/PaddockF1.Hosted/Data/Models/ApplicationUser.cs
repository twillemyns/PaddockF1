using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PaddockF1.Hosted.Data.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public IEnumerable<Topic> Topics { get; set; } = new List<Topic>();
    
    public IEnumerable<Message> Messages { get; set; } = new List<Message>();

    [NotMapped]
    public IEnumerable<string> Roles { get; set; } = new List<string>();
}