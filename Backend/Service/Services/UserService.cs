using Domain;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingWebApi.Data.Models;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _unitOfWork.User.GetAllUserAsync();
        }

        public async Task<User?> GetUserById(string id)
        {
            return await _unitOfWork.User.GetUserByIdAsync(id);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _unitOfWork.User.GetUserByEmailAsync(email);
        }

        public async Task<IdentityResult> CreateUser(User newUser, string password)
        {
            var result = await _unitOfWork.User.AddUserAsync(newUser, password);
            if (result.Succeeded)
            {
                await _unitOfWork.User.AddToRoleAsync(newUser, "User");
            }

            return result;
        }
        public async Task<IdentityResult> CreateUser(User user)
        {
            var result = await _unitOfWork.User.AddUserAsync(user);
            if (result.Succeeded)
            {
                await _unitOfWork.User.AddToRoleAsync(user, "User");
            }

            await _unitOfWork.CommitAsync();
            return result;
        }

        public async Task<bool> CheckUserPassword(User user, string password)
        {
            return await _unitOfWork.User.CheckUserPasswordAsync(user, password);
        }

        public async Task UpdateUser(User userToBeUpdated, User userData)
        {
            // оновлення даних вручну
            userToBeUpdated.FullName = userData.FullName;
            userToBeUpdated.Email = userData.Email;

            await _unitOfWork.User.UpdateAsync(userToBeUpdated);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteUser(User user)
        {
            await _unitOfWork.User.RemoveUserAsync(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> AddExperienceAsync(string userId, int xp)
        {
            var user = await _unitOfWork.User.GetUserByIdAsync(userId);
            if (user == null)
                return false;

            await _unitOfWork.User.AddExperienceAsync(user, xp);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}