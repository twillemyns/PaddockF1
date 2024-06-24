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
    /// <summary>
    /// Récupère les messages d'un sujet de discussion
    /// </summary>
    /// <param name="topicId">ID du sujet de discussion</param>
    /// <returns>Un énumérable de tous les messages du sujet de discussion</returns>
    public IEnumerable<Message> GetMessagesByTopicId(Guid topicId) => appUnit.Messages.GetAll(m => m.TopicId == topicId);

    /// <summary>
    /// Récupère un utilisateur par son ID
    /// </summary>
    /// <param name="userId">ID de l'utilisateur</param>
    /// <returns>L'utilisateur demandé</returns>
    public ApplicationUser? GetUserById(string userId) => appUnit.Users.Get(userId);

    #endregion
    
    #region Add
    
    /// <summary>
    /// Ajoute un sujet de discussion à la base de données
    /// </summary>
    public void AddTopic(Topic topic)
    {
        appUnit.Topics.Add(topic);
        appUnit.SaveChanges();
    }
    
    /// <summary>
    /// Ajoute un message à la base de données
    /// </summary>
    public void AddMessage(Message message)
    {
        appUnit.Messages.Add(message);
        appUnit.SaveChanges();
    }
    
    #endregion
    
    
    #region Delete
    
    /// <summary>
    /// Supprime un sujet de discussion de la base de données
    /// </summary>
    public void DeleteTopic(Topic topic)
    {
        appUnit.Topics.Delete(topic);
        appUnit.SaveChanges();
    }
    
    /// <summary>
    /// Supprime un message de la base de données
    /// </summary>
    public void DeleteMessage(Message message)
    {
        appUnit.Messages.Delete(message);
        appUnit.SaveChanges();
    }
    
    #endregion


}