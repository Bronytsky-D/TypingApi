using TypingWeb.Aplication.Abstractions.UseCases;
using TypingWeb.Common;
using TypingWeb.Domain;
using TypingWeb.Domain.Abstractions.Services;

namespace TypingWeb.Aplication.UseCases
{
    public class GetUserUseCase : IGetUserUseCase
    {
        private readonly IUserService _userService;
        public GetUserUseCase(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IExecutionResponse> ExecuteAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return ExecutionResponse.Failure("User ID cannot be null or empty.");
            }
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return ExecutionResponse.Failure("User not found.");
            }
            return ExecutionResponse.Successful(user);
        }
    }
}
