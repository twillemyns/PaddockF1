using PaddockF1.Abstractions;
using PaddockF1.Module.Forum.Models;

namespace PaddockF1.Module.Forum.Data;

public class TopicRepository(ForumContext context) : Repository<Topic, ForumContext>(context), IDisposable
{
    private bool _disposed;
    
    protected virtual void Dispose(bool disposing)
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