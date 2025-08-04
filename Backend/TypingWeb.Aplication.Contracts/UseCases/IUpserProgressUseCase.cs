using TypingWeb.Common;
using TypingWeb.Common.DTOs;

namespace TypingWeb.Aplication.Abstractions.UseCases
{
    public interface IUpserProgressUseCase
    {
        Task<IExecutionResponse> ExecuteAsync(ProgressWriteRequestDto progress);
    }
}
