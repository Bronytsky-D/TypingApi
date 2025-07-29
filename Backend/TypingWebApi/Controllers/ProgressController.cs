using AutoMapper;
using Domain.Models;
using Domain.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypingWebApi.Domains.Models.Types;
using TypingWebApi.Dtos;

namespace TypingWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly IProgressService _progressService;
        private readonly IMapper _mapper;
        private readonly IValidator<ProgressWriteRequestDto> _validator;
        public ProgressController(IProgressService progressService, IMapper mapper, 
            IValidator<ProgressWriteRequestDto> validator)
        {
            _progressService = progressService;
            _mapper = mapper;
            _validator = validator;
        }
           
        [HttpGet("{userId}/lesson/{lessonId}")]
        public async Task<IExecutionResponse> GetByUser(string userId, int lessonId)
        {
           return await _progressService.GetByUserAndLessonIdAsync(userId, lessonId);
        }

        [HttpGet("{userId}")]
        public async Task<IExecutionResponse> GetByUserAndLesson(string userId)
        {
            return await _progressService.GetByUserIdAsync(userId);
        }

        [HttpPost]
        public async Task<IExecutionResponse> Upser(ProgressWriteRequestDto progresDto)
        {
            var validationResult = await _validator.ValidateAsync(progresDto);
            if (!validationResult.IsValid)
            {
                var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                {
                    Type = "https://example.com/validation-error",
                    Title = "Validation Error",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "One or more validation errors occurred."
                };
                return ExecutionResponse.Failure(problemDetails.Detail);
            }
            var entity = _mapper.Map<LessonProgress>(progresDto);
            return await _progressService.UpsertAsync(entity);
        }
    }
}
