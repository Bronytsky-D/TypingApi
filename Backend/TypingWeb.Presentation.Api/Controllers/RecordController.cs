using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypingWebApi.Dtos;

using TypingWeb.Domain;
using TypingWeb.Domain.Abstractions.Services;

using TypingWeb.Common;
using TypingWeb.Domain.Models.Entities;

namespace TypingWebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _recordService;
        private readonly IUserGameService _userGameService;
        private readonly IMapper _mapper; 
        private readonly IValidator<RecordWriteRequestDto> _validator;

        public RecordController(IRecordService recordService, 
            IUserGameService userGameService,
            IMapper mapper,
            IValidator<RecordWriteRequestDto> validator)
        {
            _recordService = recordService;
            _mapper = mapper;
            _validator = validator;
            _userGameService = userGameService;
        }

        [HttpPost]
        public async Task<IExecutionResponse> PostRecord(RecordWriteRequestDto recordDto)
        {
            var validationResult = await _validator.ValidateAsync(recordDto);
            if (!validationResult.IsValid)
            {
                var problemDatails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                {
                    Type = "https://example.com/validation-error",
                    Title = "Validation Error",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "One or more validation errors occurred."
                };
                return ExecutionResponse.Failure(problemDatails.Detail);
            }
            var record = _mapper.Map<RecordEntity>(recordDto);
            var response = await _recordService.AddRecordAsync(record);

            if (response.Success && recordDto.Experience > 0)
            {
                await _userGameService.AddExperienceAsync(recordDto.UserId, recordDto.Experience);
            }

            return response;
        }

        [HttpGet("{userId}")]
        public async Task<IExecutionResponse> GetRecordsByUser(string userId)
        {
            return await _recordService.GetRecordsByUserIdAsync(userId);
        }

    } 
}
