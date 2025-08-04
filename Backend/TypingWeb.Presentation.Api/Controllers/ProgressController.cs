using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypingWeb.Common;
using TypingWeb.Common.DTOs;
using TypingWeb.Aplication.Abstractions.UseCases;

namespace TypingWebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly IGetProgressUseCase _getProgressUseCase;
        private readonly IUpserProgressUseCase _upserProgressUseCase;
        private readonly IGetProgressByUserAndLessonUseCase _getProgressByUserAndLessonUseCase;
        public ProgressController( IGetProgressUseCase getProgressUseCase,
            IUpserProgressUseCase upserProgressUseCase,
            IGetProgressByUserAndLessonUseCase getProgressByUserAndLessonUseCase)
        {
            _getProgressByUserAndLessonUseCase = getProgressByUserAndLessonUseCase;
            _getProgressUseCase = getProgressUseCase;
            _upserProgressUseCase = upserProgressUseCase;  
        }
           
        [HttpGet("{userId}/lesson/{lessonId}")]
        public async Task<IExecutionResponse> GetByUserAndLesson(string userId, int lessonId)
        {
           return await _getProgressByUserAndLessonUseCase.ExecuteAsync(userId, lessonId);
        }

        [HttpGet("{userId}")]
        public async Task<IExecutionResponse> GetByUser(string userId)
        {
            return await _getProgressUseCase.ExecuteAsync(userId);
        }

        [HttpPost]
        public async Task<IExecutionResponse> Upser(ProgressWriteRequestDto progresDto)
        {
            return await _upserProgressUseCase.ExecuteAsync(progresDto);
        }
    }
}
