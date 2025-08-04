using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypingWeb.Common;
using TypingWeb.Aplication.Abstractions.UseCases;

namespace TypingWeb.Presentation.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IGetUserUseCase _getUserUseCase;
        public UserController(IGetUserUseCase getUserUseCase)
        {
            _getUserUseCase = getUserUseCase;
        }

        [HttpGet("{userId}")]
        public async Task<IExecutionResponse> GetUser(string userId)
        {
            return await _getUserUseCase.ExecuteAsync(userId);
        }
    }
}
