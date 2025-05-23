using Domain.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TypingWebApi.Data.Models;
using TypingWebApi.Domains.Models.Types;
using TypingWebApi.Dtos;
using TypingWebApi.Service;
using static Google.Apis.Requests.BatchRequest;

namespace TypingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _recordService;
        private readonly IUserService _userService;

        public RecordController(IRecordService recordService, IUserService userService)
        {
            _recordService = recordService;
            _userService = userService;
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
                MatchTime = recordDto.MatchTime,
                GameLength = recordDto.GameLength,
                Mode = recordDto.Mode
            };
            var response = await _recordService.AddRecordAsync(record);

            if (response.Success && recordDto.Experience > 0)
            {
                await _userService.AddExperienceAsync(recordDto.userId, recordDto.Experience);
            }
            record.User = null;

            return response;
        }

        [HttpGet("read/{userId}")]
        public async Task<IExecutionResponse> GetRecordsByUserId(string userId)
        {
            return await _recordService.GetRecordsByUserIdAsync(userId);
        }

    } 
}
