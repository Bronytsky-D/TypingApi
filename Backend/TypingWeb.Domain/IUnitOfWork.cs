using Domain.Repositories;

namespace Domain.Models.Types
{
    public interface IUnitOfWork: IDisposable
    {
        IRecordRepository Record { get; }
        ITokenRepository RefreshToken { get; }
        IProgressRepository Progress { get; }
        Task<int> CommitAsync();
    }
}
