using Domain;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingWebApi.Data.Context;
using TypingWebApi.Data.Models;

namespace Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        private RecordRepository _recordRepository;
        private UserRepository _userRepository;
        private TokenRepository _refreshTokenRepository;

        public UnitOfWork(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IRecordRepository Record => _recordRepository ??= new RecordRepository(_context);
        public IUserRepository User => _userRepository ??= new UserRepository(_userManager);
        public ITokenRepository RefreshToken => _refreshTokenRepository??= new TokenRepository(_context);
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
