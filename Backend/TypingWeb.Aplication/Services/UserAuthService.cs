using Microsoft.AspNetCore.Identity;
using TypingWeb.Domain.Models.Entities;
using TypingWeb.Domain.Abstractions.Services;
using TypingWeb.Infrastructure.PostgreSQL.Models;
using AutoMapper;

namespace TypingWeb.Service.Services
{
    //public class UserAuthService : IUserAuthService
    //{
    //    private readonly UserManager<UserProfile> _userManager;
    //    private readonly IMapper _mapper;
    //    public UserAuthService(UserManager<UserProfile> userManager, IMapper autoMapper)
    //    {
    //        _userManager = userManager;
    //        _mapper = autoMapper;
    //    }
    //    public async Task<bool> CheckUserPassword(UserProfileEntity model, string password)
    //    {
    //        var entity = _mapper.Map<UserProfile>(model);

    //        return await _userManager.CheckPasswordAsync(entity, password);
    //    }
    //}
}
