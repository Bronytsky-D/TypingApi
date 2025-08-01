using System.Collections.Generic;

namespace TypingWeb.Common
{
    public interface IExecutionResponse
    {
        bool Success { get; }
        IEnumerable<string> Errors { get; }
        object Result { get; }
    }
}
