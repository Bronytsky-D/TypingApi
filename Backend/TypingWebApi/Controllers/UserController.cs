using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.Services;
using TypingWebApi.Dtos;

namespace TypingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(
           IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile/{userId}")]
        public async Task<ActionResult<UserGetDto>> GetProfile(string userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
                return NotFound();

            var dto = new UserGetDto
            {
                FullName = user.FullName,
                Level = user.Level,
                ExperiencePoints = user.ExperiencePoints,
                ExperienceToNextLevel = 100 * (user.Level + 1),
                Achievements = user.Achievements ?? new List<string>()
            };

            return Ok(dto);
        }
    }
}
