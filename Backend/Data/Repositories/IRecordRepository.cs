using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingWebApi.Data.Models;

namespace Domain.Repositories
{
    public interface IRecordRepository: IRepository<Record>
    {
        Task<IEnumerable<Record>> GetRecordsByUserIdAsync(string userId);
        public Task<IEnumerable<Record>> GetAllRecordAsync();
        public Task AddRecordAsync(Record entity);
        public void RemoveRecordAsync(Record entity);
    }
}
