using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.Types;
using Repository.ExecutionResponse;

namespace TypingWebApi.Controllers
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
            var user = await _userService.GetUserById(userId);
            if (user == null)
                return ExecutionResponse.Failure("not founde");
            return user;
        }
    }
}
