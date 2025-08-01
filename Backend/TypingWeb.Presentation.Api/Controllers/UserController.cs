using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypingWeb.Domain;
using TypingWeb.Domain.Abstractions.Services;
using TypingWeb.Common;

namespace TypingWeb.Presentation.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IExecutionResponse> GetUser(string userId)
        {
            //var user = await _userService.GetUserById(userId);
            //if (user == null)
            //    return ExecutionResponse.Failure("not founde");
            //return user;

            return ExecutionResponse.Successful(true);
        }
    }
}
