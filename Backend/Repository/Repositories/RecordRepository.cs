using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingWebApi.Data;
using TypingWebApi.Data.Context;
using TypingWebApi.Data.Models;

namespace Repository.Repositories
{
    public class RecordRepository: Repository<Record>,IRecordRepository
    {
        public RecordRepository(ApplicationContext context)
            : base(context)
        { }
        public async Task<IEnumerable<Record>> GetRecordsByUserIdAsync(string userId)
        {
            return await Context.Records
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Record>> GetAllRecordAsync()
        {
            return await Context.Records.ToListAsync();
        }
        public async Task AddRecordAsync(Record entity)
        {
            await Context.Records.AddAsync(entity);
        }
        public async void RemoveRecordAsync(Record entity)
        {
            Context.Records.Remove(entity);
            await Context.SaveChangesAsync();
        }

        private ApplicationContext Context => (ApplicationContext)_context;

    }
}
