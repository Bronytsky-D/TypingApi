using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TypingWeb.Common;
using TypingWeb.Domain;
using TypingWeb.Domain.Abstractions.Services;
using TypingWeb.Domain.Models.Entities;
using TypingWeb.Infrastructure;
using TypingWeb.Infrastructure.PostgreSQL.Models;

namespace TypingWeb.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = autoMapper;
        }

        public async Task<IExecutionResponse> GetAllUsers()
        {
            var users = await _unitOfWork.Users.ToListAsync();
            if (users == null || !users.Any())
            {
                return ExecutionResponse.Failure("No users found");
            }

            return ExecutionResponse.Successful(users);
        }

        public async Task<IExecutionResponse> GetUserById(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return ExecutionResponse.Failure("User not found");
            }

            return ExecutionResponse.Successful(user);
        }

        public async Task<IExecutionResponse> GetUserByEmail(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return ExecutionResponse.Failure("User not found");
            }

            return ExecutionResponse.Successful(user);
        }

        public async Task<IExecutionResponse> CreateUser(UserProfileEntity model, string? password = null)
        {
            var entity = _mapper.Map<UserProfile>(model);
            IdentityResult identityResult = password == null
                ? await _userManager.CreateAsync(entity)
                : await _userManager.CreateAsync(entity, password);

            if (!identityResult.Succeeded)
            {
                var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
                return ExecutionResponse.Failure(errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(entity, "User");

            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                return ExecutionResponse.Failure(errors);
            }

            return ExecutionResponse.Successful(entity);
        }

        public async Task<IExecutionResponse> DeleteUser(UserProfileEntity model)
        {
            var entity = _mapper.Map<UserProfile>(model);
            IdentityResult deleteResult = await _userManager.DeleteAsync(entity);
            if (!deleteResult.Succeeded)
            {
                var errors = string.Join(", ", deleteResult.Errors.Select(e => e.Description));
                return ExecutionResponse.Failure(errors);
            }

            return ExecutionResponse.Successful(entity.Id);
        }
    }
}