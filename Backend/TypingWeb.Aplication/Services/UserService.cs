using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TypingWeb.Domain.Abstractions.Services;
using TypingWeb.Common;
using TypingWeb.Domain;
using TypingWeb.Domain.Models.Entities;

namespace TypingWeb.Service.Services
{
    public class UserService : IUserService
    {
    //    private readonly UserManager<UserEntity> _userManager;

    //    public UserService(UserManager<UserEntity> userManager)
    //    {
    //        _userManager = userManager;
    //    }

    //    public async Task<IExecutionResponse> GetAllUsers()
    //    {
    //        var users = await _userManager.Users.ToListAsync();
    //        if(users == null || !users.Any())
    //        {
    //            return ExecutionResponse.Failure("No users found");
    //        }
    //        return ExecutionResponse.Successful(users);
    //    }

    //    public async Task<IExecutionResponse> GetUserById(string id)
    //    {
    //        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
    //        if (user == null)
    //        {
    //            return ExecutionResponse.Failure("User not found");
    //        }
    //        return ExecutionResponse.Successful(user);
    //    }

    //    public async Task<IExecutionResponse> GetUserByEmail(string email)
    //    {
    //        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
    //        if (user == null)
    //        {
    //            return ExecutionResponse.Failure("User not found");
    //        }
    //        return ExecutionResponse.Successful(user);
    //    }

    //    public async Task<IExecutionResponse> CreateUser(UserEntity newUser, string ?password = null)
    //    {
    //        IdentityResult identityResult = password == null
    //            ?await _userManager.CreateAsync(newUser)
    //            :await _userManager.CreateAsync(newUser, password);

    //        if (!identityResult.Succeeded)
    //        {
    //            var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
    //            return ExecutionResponse.Failure(errors);
    //        }

    //        var roleResult = await _userManager.AddToRoleAsync(newUser, "User");

    //        if (!roleResult.Succeeded)
    //        {
    //            var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
    //            return ExecutionResponse.Failure(errors);
    //        }
    //        return ExecutionResponse.Successful(newUser);
    //    }

    //    public async Task<IExecutionResponse> DeleteUser(UserEntity user)
    //    {
    //        IdentityResult deleteResult = await _userManager.DeleteAsync(user);
    //        if (!deleteResult.Succeeded)
    //        {
    //            var errors = string.Join(", ", deleteResult.Errors.Select(e => e.Description));
    //            return ExecutionResponse.Failure(errors);
    //        }
    //        return ExecutionResponse.Successful(null);
    //    }
    }
}