using Microsoft.EntityFrameworkCore;
using PaddockF1.Abstractions;
using PaddockF1.Hosted.Data.Repositories;

namespace PaddockF1.Hosted.Data;

/// <summary>
/// Dépôt des données de l'application
/// </summary>
/// <param name="context"><inheritdoc cref="ApplicationDbContext"/></param>
public class ApplicationUnit(ApplicationDbContext context) : IUnitOfWork
{
    private TopicRepository? _topics;
    
    private MessageRepository? _messages;

    private UserRepository? _users;

    /// <summary>
    /// Dépôt des sujets de discussion
    /// </summary>
    public TopicRepository Topics
    {
        get
        {
            _topics ??= new TopicRepository(context);

            return _topics;
        }
    }
    
    /// <summary>
    /// Dépôt des messages
    /// </summary>
    public MessageRepository Messages
    {
        get
        {
            _messages ??= new MessageRepository(context);

            return _messages;
        }
    }
    
    /// <summary>
    /// Dépôt des utilisateurs
    /// </summary>
    public UserRepository Users
    {
        get
        {
            _users ??= new UserRepository(context);

            return _users;
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

    /// <summary>
    /// Sauvegarde les modifications effectuées en base de données
    /// </summary>
    /// <returns>Le nombre de modifications</returns>
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