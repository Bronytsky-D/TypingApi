using AutoMapper;
using Domain.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.Types;
using TypingWebApi.Dtos;
using TypingWebApi.Service;
using Repository.ExecutionResponse;
using Domain.Models;

namespace TypingWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _recordService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper; 
        private readonly IValidator<RecordWriteRequestDto> _validator;

        public RecordController(IRecordService recordService, IUserService userService, IMapper mapper,
            IValidator<RecordWriteRequestDto> validator)
        {
            _recordService = recordService;
            _userService = userService;
            _mapper = mapper;
            _validator = validator;
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
            var record = _mapper.Map<Record>(recordDto);
            var response = await _recordService.AddRecordAsync(record);

            if (response.Success && recordDto.Experience > 0)
            {
                await _userService.AddExperienceAsync(recordDto.UserId, recordDto.Experience);
            }
            record.User = null;

            return response;
        }

        [HttpGet("{userId}")]
        public async Task<IExecutionResponse> GetRecordsByUser(string userId)
        {
            return await _recordService.GetRecordsByUserIdAsync(userId);
        }

    } 
}
