using AutoMapper;
using Domain.Models;
using Domain.Models.Types;
using Domain.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Repository.ExecutionResponse;
using System.IdentityModel.Tokens.Jwt;
using TypingWeb.Auth.Api.Dtos;

namespace TypingWeb.Auth.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserAuthService _userAuthService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IValidator<LoginRequestDto> _validatorLogin;
        private readonly IValidator<RegisterRequestDto> _validatorRegister;

        public AuthController(
           IUserService userService,
           IUserAuthService userAuthService,
           ITokenService tokenService,
           IMapper mapper,
           IValidator<LoginRequestDto> validatorLogin,
           IValidator<RegisterRequestDto> validatorRegister)
        {
            _userService = userService;
            _userAuthService = userAuthService;
            _tokenService = tokenService;
            _mapper = mapper;
            _validatorLogin = validatorLogin;
            _validatorRegister = validatorRegister;
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

        [HttpPost("register")]
        public async Task<IExecutionResponse> Register(RegisterRequestDto dto)
        {
            var validationResult = await _validatorRegister.ValidateAsync(dto);
            if(!validationResult.IsValid)
            {
                var problemDatails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                {
                    Type = "https://example.com/validation-error",
                    Title = "Validation Error",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "One or more validation errors occurred."
                };
                return ExecutionResponse.Failure(problemDatails.Detail);
            }

            var user = _mapper.Map<User>(dto);

            var result = await _userService.CreateUser(user, dto.Password);
            if (!result.Success)
                return result;

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            await _tokenService.SaveRefreshTokenAsync(user, refreshToken);

            SetRefreshTokenCookie(refreshToken);

            return ExecutionResponse.Successful(accessToken);
        }
        [HttpPost("login")]
        public async Task<IExecutionResponse> Login(LoginRequestDto dto)
        {
            var validationResult = await _validatorLogin.ValidateAsync(dto);
            if(!validationResult.IsValid)
            {
                var problemDatails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                {
                    Type = "https://example.com/validation-error",
                    Title = "Validation Error",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "One or more validation errors occurred."
                };
                return ExecutionResponse.Failure(problemDatails.Detail);
            }

            var userRespone = await _userService.GetUserByEmail(dto.Email);
            var user = userRespone.Result as User;
            if (user == null)
                return ExecutionResponse.Failure("User not found");

            var result = await _userAuthService.CheckUserPassword(user, dto.Password);
            if (!result)
                return ExecutionResponse.Failure("Invalid Password");

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            await _tokenService.SaveRefreshTokenAsync(user, refreshToken);

            SetRefreshTokenCookie(refreshToken);

            return ExecutionResponse.Successful(accessToken);
        }
        [HttpPost("refresh")]
        public async Task<IExecutionResponse> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return ExecutionResponse.Failure("Refresh token not found");

            var userId = GetUserIdFromExpiredAccessToken();
            if (string.IsNullOrEmpty(userId))
                return ExecutionResponse.Failure("UserId not found");

            var token = await _tokenService.GetValidRefreshTokenAsync(userId, refreshToken);
            if (token == null)
                return ExecutionResponse.Failure("Invalid refresh token");

            token.IsRevoked = true;

            var userRespone = await _userService.GetUserById(userId);
            var user = userRespone.Result as User;
            if (user == null)
                return ExecutionResponse.Failure("User not found");

            var newAccess = _tokenService.GenerateAccessToken(user);
            var newRefresh = _tokenService.GenerateRefreshToken();
            await _tokenService.SaveRefreshTokenAsync(user, newRefresh);

            SetRefreshTokenCookie(newRefresh);

            return ExecutionResponse.Successful(newAccess);
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("refreshToken");
            return Ok("Logged out");
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
        //[HttpPost("google-login")]
        //public async Task<ActionResult<AuthResponseDto>> GoogleLogin([FromBody] GoogleLoginRequestDto dto)
        //{
        //    try
        //    {
        //        var settings = new GoogleJsonWebSignature.ValidationSettings()
        //        {
        //            Audience = new List<string> { _configuration["Authentication:Google:ClientId"] }
        //        };

        //        var payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken, settings);

        //        var user = await _userService.GetUserByEmail(payload.Email);

        //        if (user == null)
        //        {
        //            user = new User
        //            {
        //                Email = payload.Email,
        //                UserName = payload.Email,
        //                FullName = payload.Name,
        //                EmailConfirmed = true
        //            };

        //            var result = await _userService.CreateUser(user);
        //            if (!result.Succeeded)
        //            {
        //                return BadRequest(new AuthResponseDto
        //                {
        //                    IsSuccess = false,
        //                    Message = "Failed to create user from Google account"
        //                });
        //            }
        //        }

        //        var accessToken = _tokenService.GenerateAccessToken(user);
        //        var refreshToken = _tokenService.GenerateRefreshToken();
        //        await _tokenService.SaveRefreshTokenAsync(user, refreshToken);

        //        SetRefreshTokenCookie(refreshToken);

        //        return Ok(new AuthResponseDto
        //        {
        //            Token = accessToken,
        //            IsSuccess = true,
        //            Message = "Google login success"
        //        });
        //    }
        //    catch (InvalidJwtException)
        //    {
        //        return BadRequest(new AuthResponseDto
        //        {
        //            IsSuccess = false,
        //            Message = "Invalid Google ID token"
        //        });
        //    }
        //}
    }
}
