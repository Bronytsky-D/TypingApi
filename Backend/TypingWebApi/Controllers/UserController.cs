using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using TypingWebApi.Dtos;

namespace TypingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;  
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserReadResponeDto>> GetUser(string userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
                return NotFound();
            var dto = _mapper.Map<UserReadResponeDto>(user);

            return Ok(dto);
        }
    }
}
