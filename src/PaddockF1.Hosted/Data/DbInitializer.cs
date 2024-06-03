using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using PaddockF1.Module.Forum.Data;
using PaddockF1.Module.Forum.Models;

namespace PaddockF1.Hosted.Data;

public static class DbInitializer
{
    
    public static void Initialize(ApplicationDbContext appDbContext, ForumContext forumDbContext)
    {
        appDbContext.Database.EnsureCreated();

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
                Email = "admin@admin.com",
                EmailConfirmed = true
                
            },
            new()
            {
                UserName = "user",
                Email = "user@user.com",
                EmailConfirmed = true
            }
        };

        foreach (var user in users)
        {
            user.PasswordHash = passwordHasher.HashPassword(user, user.UserName!);
        }
        
        appDbContext.Users.AddRange(users);
        appDbContext.SaveChanges();
        
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
        
        forumDbContext.Topics.AddRange(topics);
        
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
        
        forumDbContext.Messages.AddRange(messages);
        forumDbContext.SaveChanges();

    }
}