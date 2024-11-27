using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaddockF1.Hosted.Data;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Tests;

[TestClass]
public class ApplicationServiceTests
{
    private SqliteConnection _connection = null!;

    private ApplicationDbContext _dbContext = null!;

    private ApplicationService _service = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _connection = new SqliteConnection($"DataSource={Path.GetTempFileName()}");
        _connection.Open();
        _dbContext =
            new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(_connection)
                .Options);
        _dbContext.Database.EnsureCreated();
        _service = new ApplicationService(_dbContext);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _connection.Close();
        _connection.Dispose();
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    [TestMethod]
    public void GetTopicById_ReturnCorrectTopic()
    {
        // Arrange
        var author = new ApplicationUser { UserName = "Test" };
        var topic = new Topic { Title = "Test", Description = "Test", Author = author };
        _dbContext.Topics.Add(topic);
        _dbContext.SaveChanges();

        // Act
        var result = _service.GetTopicById(topic.Id);

        // Assert
        Assert.AreEqual(topic, result);
    }

    [TestMethod]
    public void GetTopicById_ReturnNull()
    {
        // Act
        var result = _service.GetTopicById(Guid.NewGuid());

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public void GetTopics_ReturnAllTopics()
    {
        // Arrange
        var author = new ApplicationUser { UserName = "Test" };
        var topics = new[]
        {
            new Topic { Title = "Test1", Description = "Test1", Author = author },
            new Topic { Title = "Test2", Description = "Test2", Author = author }
        };
        _dbContext.Topics.AddRange(topics);
        _dbContext.SaveChanges();

        // Act
        var result = _service.GetTopics();

        // Assert
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public void GetTopics_ReturnFilteredTopics()
    {
        // Arrange
        var author = new ApplicationUser { UserName = "Test" };
        var topics = new[]
        {
            new Topic { Title = "Test1", Description = "Test1", Author = author },
            new Topic { Title = "Test2", Description = "Test2", Author = author }
        };
        _dbContext.Topics.AddRange(topics);
        _dbContext.SaveChanges();

        // Act
        var result = _service.GetTopics("Test1");

        // Assert
        Assert.AreEqual(1, result.Count());
    }

    [TestMethod]
    public void GetMessagesByTopicId_ReturnAllMessages()
    {
        // Arrange
        var author = new ApplicationUser { UserName = "Test" };
        var topic = new Topic { Title = "Test", Description = "Test", Author = author };
        var messages = new[]
        {
            new Message { Content = "Test1", User = author, Topic = topic },
            new Message { Content = "Test2", User = author, Topic = topic }
        };
        _dbContext.Messages.AddRange(messages);
        _dbContext.SaveChanges();

        // Act
        var result = _service.GetMessagesByTopicId(topic.Id);

        // Assert
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public void GetUserById_ReturnCorrectUser()
    {
        // Arrange
        var user = new ApplicationUser { UserName = "Test" };
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        // Act
        var result = _service.GetUserById(user.Id);

        // Assert
        Assert.AreEqual(user, result);
    }

    [TestMethod]
    public void GetUserById_ReturnNull()
    {
        // Act
        var result = _service.GetUserById(Guid.NewGuid().ToString());

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public void AddTopic_AddsTopicCorrectly()
    {
        // Arrange
        var author = new ApplicationUser { UserName = "Test" };
        var topic = new Topic { Title = "Test", Description = "Test", Author = author };

        // Act
        _service.AddTopic(topic);

        // Assert
        Assert.AreEqual(topic, _dbContext.Topics.First());
    }

    [TestMethod]
    public void AddMessage_AddsMessageCorrectly()
    {
        // Arrange
        var author = new ApplicationUser { UserName = "Test" };
        var topic = new Topic { Title = "Test", Description = "Test", Author = author };
        var message = new Message { Content = "Test", User = author, Topic = topic };

        // Act
        _service.AddMessage(message);

        // Assert
        Assert.AreEqual(message, _dbContext.Messages.First());
    }

    [TestMethod]
    public void DeleteTopic_DeletesTopicCorrectly()
    {
        // Arrange
        var author = new ApplicationUser { UserName = "Test" };
        var topic = new Topic { Title = "Test", Description = "Test", Author = author };
        _dbContext.Topics.Add(topic);
        _dbContext.SaveChanges();

        // Act
        _service.DeleteTopic(topic);
        _service.SaveChanges();

        // Assert
        Assert.AreEqual(0, _dbContext.Topics.Count());
    }

    [TestMethod]
    public void DeleteMessage_DeletesMessageCorrectly()
    {
        // Arrange
        var author = new ApplicationUser { UserName = "Test" };
        var topic = new Topic { Title = "Test", Description = "Test", Author = author };
        var message = new Message { Content = "Test", User = author, Topic = topic };
        _dbContext.Messages.Add(message);
        _dbContext.SaveChanges();

        // Act
        _service.DeleteMessage(message);
        _service.SaveChanges();

        // Assert
        Assert.AreEqual(0, _dbContext.Messages.Count());
    }
}