using PaddockF1.Module.Forum.Models;

namespace PaddockF1.Module.Forum.Data;

internal sealed class ForumService(UnitForum unitForum)
{
    #region Read

    public Topic? GetTopicById(Guid id) => unitForum.Topics.Get(id);
    
    public IEnumerable<Topic> GetTopics() => unitForum.Topics.GetAll();
    
    public IEnumerable<Topic> GetTopics(string search) => unitForum.Topics.GetAll(t => t.Title.Contains(search));
    
    // todo: ajout pagination voire virtualisation
    public IEnumerable<Message> GetMessagesByTopicId(Guid topicId) => unitForum.Messages.GetAll(m => m.TopicId == topicId);

    #endregion
    
    #region Add
    
    public void AddTopic(Topic topic)
    {
        unitForum.Topics.Add(topic);
        unitForum.SaveChanges();
    }
    
    public void AddMessage(Message message)
    {
        unitForum.Messages.Add(message);
        unitForum.SaveChanges();
    }
    
    #endregion
    
    
    #region Delete
    
    public void DeleteTopic(Topic topic)
    {
        unitForum.Topics.Delete(topic);
        unitForum.SaveChanges();
    }
    
    public void DeleteMessage(Message message)
    {
        unitForum.Messages.Delete(message);
        unitForum.SaveChanges();
    }
    
    #endregion
}