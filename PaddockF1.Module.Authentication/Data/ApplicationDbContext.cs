using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PaddockF1.Module.Authentication.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Ignore(m => m.PhoneNumber);
                entity.Ignore(m => m.PhoneNumberConfirmed);
                entity.HasMany(m => m.Topics).WithOne().HasForeignKey(m => m.AuthorId);
                entity.HasMany(m => m.Messages).WithOne().HasForeignKey(m => m.UserId);
            });
        }
    }
}
