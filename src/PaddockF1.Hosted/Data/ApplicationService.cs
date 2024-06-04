using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Data;

internal sealed class ApplicationService(ApplicationUnit appUnit)
{
    #region Get

    public Topic? GetTopicById(Guid id) => appUnit.Topics.Get(id);
    
    public IEnumerable<Topic> GetTopics() => appUnit.Topics.GetAll();
    
    public IEnumerable<Topic> GetTopics(string search) => appUnit.Topics.GetAll(t => t.Title.Contains(search));
    
    // todo: ajout pagination voire virtualisation
    public IEnumerable<Message> GetMessagesByTopicId(Guid topicId) => appUnit.Messages.GetAll(m => m.TopicId == topicId);

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