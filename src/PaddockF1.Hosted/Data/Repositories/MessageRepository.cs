using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PaddockF1.Abstractions;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Data.Repositories;

public sealed class MessageRepository(ApplicationDbContext context)
    : Repository<Message, ApplicationDbContext>(context), IDisposable
{
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

    public override IEnumerable<Message> GetAll(Expression<Func<Message, bool>> predicate)
    {
        return _context.Messages.Where(predicate).Include(m => m.User);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}