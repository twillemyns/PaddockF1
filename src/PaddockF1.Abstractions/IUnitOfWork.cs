namespace PaddockF1.Abstractions;

public interface IUnitOfWork : IDisposable
{
    int SaveChanges();
}