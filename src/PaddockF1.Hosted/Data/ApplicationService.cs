using Microsoft.EntityFrameworkCore;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Data;

/// <summary>
/// Service permettant de récupérer et de manipuler les données de l'application
/// </summary>
/// <param name="dbContext"></param>
public sealed class ApplicationService(ApplicationDbContext dbContext)
{
    #region Get

    /// <summary>
    /// Permet de récupérer un sujet de discussion par son <paramref name="id"/>
    /// </summary>
    /// <param name="id">ID du <see cref="Topic"/></param>
    /// <returns>Le <see cref="Topic"/> correspondant</returns>
    public Topic? GetTopicById(Guid id) => dbContext.Topics.Find(id);

    /// <summary>
    /// Réccupère tous les sujets de discussion
    /// </summary>
    public IEnumerable<Topic> GetTopics() => dbContext.Topics;

    /// <summary>
    /// Permet de récupérer les sujets de discussion dont le titre commence par <paramref name="search"/>
    /// </summary>
    /// <param name="search">Recherche du sujet</param>
    /// <returns>Une liste réduite des sujets de discussions</returns>
    public IEnumerable<Topic> GetTopics(string search) => dbContext.Topics.Where(t => t.Title.StartsWith(search));

    // todo: ajout pagination voire virtualisation
    public IEnumerable<Message> GetMessagesByTopicId(Guid topicId) =>
        dbContext.Messages.Include(m => m.User).Where(m => m.TopicId == topicId);

    public ApplicationUser? GetUserById(string userId) => dbContext.Users.Find(userId);

    #endregion

    #region Add

    public void AddTopic(Topic topic)
    {
        dbContext.Topics.Add(topic);
        dbContext.SaveChanges();
    }

    public void AddMessage(Message message)
    {
        dbContext.Messages.Add(message);
        dbContext.SaveChanges();
    }

    #endregion


    #region Delete

    public void DeleteTopic(Topic topic) => dbContext.Topics.Remove(topic);

    public void DeleteMessage(Message message) => dbContext.Messages.Remove(message);

    #endregion

    public int SaveChanges() => dbContext.SaveChanges();
}