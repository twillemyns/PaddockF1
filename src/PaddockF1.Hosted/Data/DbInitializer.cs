using Microsoft.AspNetCore.Identity;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Data;

public static class DbInitializer
{
    
    public static async Task InitializeData(ApplicationDbContext appDbContext, UserManager<ApplicationUser> userManager)
    {
        await appDbContext.Database.EnsureCreatedAsync();

        if (appDbContext.Users.Any())
        {
            return;
        }
        
        var passwordHasher = new PasswordHasher<ApplicationUser>();
        
        
        var users = new ApplicationUser[]
        {
            new()
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                EmailConfirmed = true
            },
            new()
            {
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@user.com",
                EmailConfirmed = true
            }
        };

        foreach (var user in users)
        {
            await userManager.CreateAsync(user, user.UserName!);
        }
        
        foreach (var user in users)
        {
            user.PasswordHash = passwordHasher.HashPassword(user, user.UserName!);
        }
        
        appDbContext.Users.AddRange(users);
        await appDbContext.SaveChangesAsync();
        
        var topics = new Topic[]
        {
            new()
            {
                Title = "Welcome to the forum",
                Description = "This is the first topic of the forum",
                CreatedAt = DateTime.Now,
                AuthorId = users[0].Id
            },
            new()
            {
                Title = "Second topic",
                Description = "This is the second topic of the forum",
                CreatedAt = DateTime.Now,
                AuthorId = users[1].Id
            }
        };
        
        appDbContext.Topics.AddRange(topics);
        
        var messages = new Message[]
        {
            new()
            {
                Content = "Hello, welcome to the forum",
                CreatedAt = DateTime.Now,
                UserId = users[0].Id,
                TopicId = topics[0].Id
            },
            new()
            {
                Content = "Hello, this is the second topic",
                CreatedAt = DateTime.Now,
                UserId = users[1].Id,
                TopicId = topics[1].Id
            },
            new()
            {
                Content = "This is a reply to the first topic",
                CreatedAt = DateTime.Now,
                UserId = users[1].Id,
                TopicId = topics[0].Id
            },
            new()
            {
                Content = "This is a reply to the second topic",
                CreatedAt = DateTime.Now,
                UserId = users[0].Id,
                TopicId = topics[1].Id
            },
            
        };
        
        appDbContext.Messages.AddRange(messages);
        await appDbContext.SaveChangesAsync();

    }
}