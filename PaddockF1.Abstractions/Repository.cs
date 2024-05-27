using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PaddockF1.Abstractions;

public class Repository<TEntity, TContext>(TContext context) : IRepository<TEntity, TContext>
    where TEntity : class
    where TContext : DbContext
{
    private readonly TContext _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <summary>
    /// <inheritdoc cref="IRepository{TEntity, TContext}.Add(TEntity)" />
    /// </summary>
    /// <param name="entity"></param>
    public virtual void Add(TEntity entity) => _context.Set<TEntity>().Add(entity);

    /// <summary>
    /// <inheritdoc cref="IRepository{TEntity, TContext}.AddRange(IEnumerable{TEntity})" />
    /// </summary>
    /// <param name="entities"></param>
    public virtual void AddRange(IEnumerable<TEntity> entities) => _context.Set<TEntity>().AddRange(entities);

    /// <summary>
    /// <inheritdoc cref="IRepository{TEntity, TContext}.Get(Guid)" />
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public virtual TEntity? Get(Guid guid) => _context.Set<TEntity>().Find(guid);

    /// <summary>
    /// <inheritdoc cref="IRepository{TEntity, TContext}.Get(Expression)" />
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual TEntity? Get(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return _context.Set<TEntity>().SingleOrDefault(predicate);
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// <inheritdoc cref="IRepository{TEntity,TContext}.GetAll()"/>
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerable<TEntity> GetAll() => _context.Set<TEntity>().AsEnumerable();

    /// <summary>
    /// <inheritdoc cref="IRepository{TEntity, TContext}.GetAll(Expression{Func{TEntity, bool}})" />
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return _context.Set<TEntity>().Where(predicate).ToList();
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public virtual void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);

    public int SaveChanges()
    {
        try
        {
            return _context.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}