using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PaddockF1.Abstractions;

public interface IRepository<TEntity, TContext>
    where TEntity : class
    where TContext : DbContext
{
    /// <summary>
    /// Retourne une entité selon <paramref name="guid" />.
    /// </summary>
    /// <param name="guid">Identifiant unique de l'entité à récupérer</param>
    /// <returns></returns>
    TEntity? Get(Guid guid);

    /// <summary>
    /// Retourne une entité selon <paramref name="predicate" />.
    /// </summary>
    /// <param name="predicate">Fonction permettant de sélectionner une entité selon une règle personnalisée</param>
    /// <returns></returns>
    TEntity? Get(Expression<Func<TEntity, bool>> predicate);
    
    /// <summary>
    /// Retourne toutes les entités.
    /// </summary>
    /// <returns></returns>
    IEnumerable<TEntity> GetAll();

    /// <summary>
    /// Retourne plusieurs entités selon <paramref name="predicate" />.
    /// </summary>
    /// <param name="predicate">Fonction permettant de sélectionner plusieurs entités selon une règle personnalisée</param>
    /// <returns></returns>
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Ajoute <paramref name="entity" /> au dépôt de données.
    /// </summary>
    /// <param name="entity">Entité à ajouter</param>
    void Add(TEntity entity);

    /// <summary>
    /// Ajoute <paramref name="entities" /> au dépôt de données.
    /// </summary>
    /// <param name="entities">Entités à ajouter</param>
    void AddRange(IEnumerable<TEntity> entities);
    
    /// <summary>
    /// Supprime <paramref name="entity" /> du dépôt de données.
    /// </summary>
    /// <param name="entity">Entité à supprimer</param>
    void Delete(TEntity entity);
}