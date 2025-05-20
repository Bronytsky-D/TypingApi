using Domain.Repositories;


namespace Domain
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository User { get; }
        IRecordRepository Record { get; }
        ITokenRepository RefreshToken { get; }

        Task<int> CommitAsync();
    }
}
