using Domain.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TypingWebApi.Data.Models;
using TypingWebApi.Dtos;

namespace TypingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AuthController(
            IUserService userService,
            ITokenService tokenService,
            IConfiguration configuration)
        {
            _userService = userService;
            _tokenService = tokenService;
            _configuration = configuration;
        }


        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                Email = userDto.Email,
                FullName = userDto.FullName,
                UserName = userDto.Email
            };

            var result = await _userService.CreateUser(user, userDto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            await _tokenService.SaveRefreshTokenAsync(user, refreshToken);

            SetRefreshTokenCookie(refreshToken);

            return Ok(new AuthResponseDto
            {
                Token = accessToken,
                IsSuccess = true,
                Message = "Account created successfully"
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.GetUserByEmail(loginDto.Email);
            if (user == null)
                return Unauthorized(new AuthResponseDto { IsSuccess = false, Message = "User not found" });

            var result = await _userService.CheckUserPassword(user, loginDto.Password);
            if (!result)
                return Unauthorized(new AuthResponseDto { IsSuccess = false, Message = "Invalid Password" });

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            await _tokenService.SaveRefreshTokenAsync(user, refreshToken);

            SetRefreshTokenCookie(refreshToken);

            return Ok(new AuthResponseDto
            {
                Token = accessToken,
                IsSuccess = true,
                Message = "Login Success"
            });
        }

        [HttpPost("google-login")]
        public async Task<ActionResult<AuthResponseDto>> GoogleLogin([FromBody] GoogleLoginRequestDto request)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string> { _configuration["Authentication:Google:ClientId"] }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

                var user = await _userService.GetUserByEmail(payload.Email);

                if (user == null)
                {
                    user = new User
                    {
                        Email = payload.Email,
                        UserName = payload.Email,
                        FullName = payload.Name,
                        EmailConfirmed = true
                    };

                    var result = await _userService.CreateUser(user);
                    if (!result.Succeeded)
                    {
                        return BadRequest(new AuthResponseDto
                        {
                            IsSuccess = false,
                            Message = "Failed to create user from Google account"
                        });
                    }
                }

                var accessToken = _tokenService.GenerateAccessToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken();
                await _tokenService.SaveRefreshTokenAsync(user, refreshToken);

                SetRefreshTokenCookie(refreshToken);

                return Ok(new AuthResponseDto
                {
                    Token = accessToken,
                    IsSuccess = true,
                    Message = "Google login success"
                });
            }
            catch (InvalidJwtException)
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid Google ID token"
                });
            }
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponseDto>> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized("Refresh token not found");

            var userId = GetUserIdFromExpiredAccessToken(); 
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found");

            var token = await _tokenService.GetValidRefreshTokenAsync(userId, refreshToken);
            if (token == null)
                return Unauthorized("Invalid refresh token");

            token.IsRevoked = true;

            var user = await _userService.GetUserById(userId);
            if (user == null)
                return Unauthorized("User not found");

            var newAccess = _tokenService.GenerateAccessToken(user);
            var newRefresh = _tokenService.GenerateRefreshToken();
            await _tokenService.SaveRefreshTokenAsync(user, newRefresh);

            SetRefreshTokenCookie(newRefresh);

            return Ok(new AuthResponseDto
            {
                Token = newAccess,
                IsSuccess = true,
                Message = "Token refreshed"
            });
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("refreshToken");
            return Ok("Logged out");
        }

        private void SetRefreshTokenCookie(string refreshToken)
        {
            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        }

        private string GetUserIdFromExpiredAccessToken()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            if (!authHeader.StartsWith("Bearer ")) return null;

            var token = authHeader["Bearer ".Length..];
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(token);
            return jwt?.Subject;
        }

    }
}
