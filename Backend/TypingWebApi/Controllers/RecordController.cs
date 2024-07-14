using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TypingWebApi.Data.Models;
using TypingWebApi.Domains.Models.Types;
using TypingWebApi.Dtos;
using TypingWebApi.Service;

namespace TypingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _recordService;

        public RecordController(IRecordService recordService) 
        {
            _recordService = recordService;
        }
        [HttpPost("write")]
        public async Task<IExecutionResponse> WriteRecord(WriteRecordDto recordDto)
        {
            var record = new Record
            {
                DateRecord = DateTime.UtcNow,
                Wpm = recordDto.Wpm,
                Raw = recordDto.Raw,
                Accuracy = recordDto.Accuracy,
                Consistency = recordDto.Consistency,
                UserId = recordDto.userId,
                Chars = recordDto.Chars,
                MatchTime = recordDto.MatchTime
            };

            return await _recordService.AddRecordAsync(record);
        }
        [HttpGet("read/{userId}")]
        public async Task<IExecutionResponse> GetRecordsByUserId(string userId)
        {
            return await _recordService.GetRecordsByUserIdAsync(userId);
        }

    } 
}
