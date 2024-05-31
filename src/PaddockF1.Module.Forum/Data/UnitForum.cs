using Microsoft.EntityFrameworkCore;
using PaddockF1.Abstractions;

namespace PaddockF1.Module.Forum.Data;

public class UnitForum(ForumContext context) : IUnitOfWork
{
    private TopicRepository? _topics;
    
    private MessageRepository? _messages;

    public TopicRepository Topics
    {
        get
        {
            _topics ??= new TopicRepository(context);

            return _topics;
        }
    }

    public MessageRepository Messages
    {
        get
        {
            _messages ??= new MessageRepository(context);

            return _messages;
        }
    }
    
    private bool _disposed;

    private void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _topics!.Dispose();
                _messages!.Dispose();
            }
        }
        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public int SaveChanges()
    {
        try
        {
            return Topics.SaveChanges() + Messages.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}