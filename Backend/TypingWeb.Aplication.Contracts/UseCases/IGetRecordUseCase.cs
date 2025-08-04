using TypingWeb.Common;

namespace TypingWeb.Aplication.Abstractions.UseCases
{
    public interface IGetRecordUseCase
    {
        Task<IExecutionResponse> ExecuteAsync(string userId);
    }
}
