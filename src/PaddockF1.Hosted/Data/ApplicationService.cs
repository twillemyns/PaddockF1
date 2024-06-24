using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Data;

/// <summary>
/// Service permettant de récupérer et de manipuler les données de l'application
/// </summary>
/// <param name="appUnit"></param>
public sealed class ApplicationService(ApplicationUnit appUnit)
{
    #region Get
    
    /// <summary>
    /// Permet de récupérer un sujet de discussion par son <paramref name="id"/>
    /// </summary>
    /// <param name="id">ID du <see cref="Topic"/></param>
    /// <returns>Le <see cref="Topic"/> correspondant</returns>
    public Topic? GetTopicById(Guid id) => appUnit.Topics.Get(id);
    
    /// <summary>
    /// Réccupère tous les sujets de discussion
    /// </summary>
    public IEnumerable<Topic> GetTopics() => appUnit.Topics.GetAll();
    
    /// <summary>
    /// Permet de récupérer les sujets de discussion dont le titre commence par <paramref name="search"/>
    /// </summary>
    /// <param name="search">Recherche du sujet</param>
    /// <returns>Une liste réduite des sujets de discussions</returns>
    public IEnumerable<Topic> GetTopics(string search) => appUnit.Topics.GetAll(t => t.Title.StartsWith(search));
    
    // todo: ajout pagination voire virtualisation
    public IEnumerable<Message> GetMessagesByTopicId(Guid topicId) => appUnit.Messages.GetAll(m => m.TopicId == topicId);

    public ApplicationUser? GetUserById(string userId) => appUnit.Users.Get(userId);

    #endregion
    
    #region Add
    
    public void AddTopic(Topic topic)
    {
        appUnit.Topics.Add(topic);
        appUnit.SaveChanges();
    }
    
    public void AddMessage(Message message)
    {
        appUnit.Messages.Add(message);
        appUnit.SaveChanges();
    }
    
    #endregion
    
    
    #region Delete
    
    public void DeleteTopic(Topic topic)
    {
        appUnit.Topics.Delete(topic);
        appUnit.SaveChanges();
    }
    
    public void DeleteMessage(Message message)
    {
        appUnit.Messages.Delete(message);
        appUnit.SaveChanges();
    }
    
    #endregion


}