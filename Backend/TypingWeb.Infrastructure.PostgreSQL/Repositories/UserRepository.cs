using AutoMapper;
using TypingWeb.Common;
using TypingWeb.Domain.Models.Entities;
using TypingWeb.Infrastructure.Repositories;

namespace TypingWeb.Infrastructure.PostgreSQL.Repositories
{
    public class UserRepository(ApplicationContext ctx, IMapper mapper) : IUserRepository
    {
        public Task<IExecutionResponse> AddAsync(UserProfileEntity entity)
        {
            var response = ctx.user
        }

        public Task<IEnumerable<UserProfileEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileEntity?> GetByUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IExecutionResponse> RemoveAsync(UserProfileEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IExecutionResponse> UpdateAsync(UserProfileEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
