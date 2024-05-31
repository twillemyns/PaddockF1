using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaddockF1.Abstractions;
using PaddockF1.Module.Forum.Data;

namespace PaddockF1.Module.Forum;

public static class ForumExtensions
{
    public static void AddForum(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ForumContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        services.AddScoped<ForumService>(provider =>
        {
            var context = provider.GetRequiredService<ForumContext>();
            return new ForumService(new UnitForum(context));
        });
    }
}