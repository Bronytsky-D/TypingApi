
using TypingWeb.Common;
using TypingWeb.Common.DTOs;

namespace TypingWeb.Aplication.Abstractions.UseCases
{
    public interface IAddRecordUseCase 
    {
        Task<IExecutionResponse> ExecuteAsync(RecordWriteRequestDto recordDto);
    }
}
