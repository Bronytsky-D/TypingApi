using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using TypingWebApi.Domains.Models.Types;
using TypingWebApi.Dtos;

namespace TypingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly IProgressService _progressService;
        public ProgressController(IProgressService progressService)
            => _progressService = progressService;

        [HttpGet("read/{userId}/{lessonId}")]
        public async Task<IExecutionResponse> ReadProgress(string userId, int lessonId)
            => await _progressService.GetAsync(userId, lessonId);

        [HttpPost("write")]
        public async Task<IExecutionResponse> WriteProgress(WriteProgressDto dto)
        {
            var entity = new LessonProgress
            {
                UserId = dto.UserId,
                LessonId = dto.LessonId,
                BestWpm = dto.BestWpm,
                BestRaw = dto.BestRaw,
                BestAccuracy = dto.BestAccuracy
            };
            return await _progressService.UpsertAsync(entity);
        }
    }
}
