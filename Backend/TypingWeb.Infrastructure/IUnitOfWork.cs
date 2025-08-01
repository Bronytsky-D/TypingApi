using TypingWeb.Infrastructure.Repositories;

namespace TypingWeb.Infrastructure
{
    public interface IUnitOfWork: IDisposable
    {
        //IRecordRepository Record { get; }
        //ITokenRepository RefreshToken { get; }
        IProgressRepository Progress { get; }
        Task<int> CommitAsync();
    }
}
