using AutoMapper;
using FluentValidation;
using TypingWeb.Aplication.Abstractions.UseCases;
using TypingWeb.Common;
using TypingWeb.Common.DTOs;
using TypingWeb.Domain;
using TypingWeb.Domain.Abstractions.Services;
using TypingWeb.Domain.Models.Entities;

namespace TypingWeb.Aplication.UseCases
{
    public class AddRecordUseCase : IAddRecordUseCase
    {
        private readonly IRecordService _recordService;
        private readonly IUserGameService _userGameService;
        private readonly IMapper _mapper;
        private readonly IValidator<RecordWriteRequestDto> _validator;
        public AddRecordUseCase(IRecordService recordService,
            IUserGameService userGameService,
            IMapper mapper,
            IValidator<RecordWriteRequestDto> validator)
        {
            _recordService = recordService;
            _mapper = mapper;
            _validator = validator;
            _userGameService = userGameService;
        }
        public async Task<IExecutionResponse> ExecuteAsync(RecordWriteRequestDto recordDto)
        {
            var validationResult = await _validator.ValidateAsync(recordDto);
            if (!validationResult.IsValid)
            {
                return ExecutionResponse.Failure(validationResult.Errors.ToString());
            }
            var record = _mapper.Map<RecordEntity>(recordDto);
            var response = await _recordService.AddRecordAsync(record);
            if (response.Success && recordDto.Experience > 0)
            {
                await _userGameService.AddExperienceAsync(recordDto.UserId, recordDto.Experience);
            }
            return response;
        }
    }
}
