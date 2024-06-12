using PaddockF1.Abstractions;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Data.Repositories;

public class UserRepository(ApplicationDbContext dbContext)
    : Repository<ApplicationUser, ApplicationDbContext>(dbContext), IDisposable
{
    public ApplicationUser? Get(string guid)
    {
        return dbContext.Users.Find(guid);
    }

    private bool _disposed;

    private void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}