using Microsoft.AspNetCore.Identity;
using TypingWeb.Domain.Models.Entities;
using TypingWeb.Domain.Abstractions.Services;
using TypingWeb.Infrastructure.PostgreSQL.Models;
using AutoMapper;

namespace TypingWeb.Service.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserAuthService(UserManager<User> userManager, IMapper autoMapper)
        {
            _userManager = userManager;
            _mapper = autoMapper;
        }
        public async Task<bool> CheckUserPassword(UserEntity model, string password)
        {
            var entity = _mapper.Map<User>(model);

            return await _userManager.CheckPasswordAsync(entity, password);
        }
    }
}
