namespace Domain.Models.Types
{
    public interface IExecutionResponse
    {
        bool Success { get; }
        IEnumerable<string> Errors { get; }
        object Result { get; }
    }
}
