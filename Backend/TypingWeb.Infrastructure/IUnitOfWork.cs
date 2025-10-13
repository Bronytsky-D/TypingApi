using TypingWeb.Infrastructure.Repositories;

namespace TypingWeb.Infrastructure
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository User { get; }
        IRecordRepository Record { get; }
        IProgressRepository Progress { get; }
        Task<int> CommitAsync();
    }
}
