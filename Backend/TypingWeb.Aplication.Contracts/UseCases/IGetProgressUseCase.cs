using TypingWeb.Common;

namespace TypingWeb.Aplication.Abstractions.UseCases
{
    public interface IGetProgressUseCase
    {
        Task<IExecutionResponse> ExecuteAsync(string userId);
    }
}
