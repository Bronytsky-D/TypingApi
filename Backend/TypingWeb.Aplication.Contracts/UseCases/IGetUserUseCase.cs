using TypingWeb.Common;

namespace TypingWeb.Aplication.Abstractions.UseCases
{
    public interface IGetUserUseCase
    {
        Task<IExecutionResponse> ExecuteAsync(string userId);
    }
}
