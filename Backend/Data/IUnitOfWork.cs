using Domain.Repositories;


namespace Domain
{
    public interface IUnitOfWork: IDisposable
    {
        IRecordRepository Record { get; }
        ITokenRepository RefreshToken { get; }
        IProgressRepository Progress { get; }
        Task<int> CommitAsync();
    }
}
