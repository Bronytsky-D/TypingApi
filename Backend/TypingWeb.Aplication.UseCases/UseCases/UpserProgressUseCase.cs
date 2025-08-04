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
    public class UpserProgressUseCase : IUpserProgressUseCase
    {
        private readonly IProgressService _progressService;
        private readonly IMapper _mapper;
        private readonly IValidator<ProgressWriteRequestDto> _validator;
        public UpserProgressUseCase(IProgressService progressService,
            IMapper mapper, 
            IValidator<ProgressWriteRequestDto> validator
            )
        {
            _progressService = progressService;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<IExecutionResponse> ExecuteAsync(ProgressWriteRequestDto progress)
        {
            var validationResult = await _validator.ValidateAsync(progress);
            if (!validationResult.IsValid)
            {
                return ExecutionResponse.Failure(validationResult.Errors.ToString());
            }
            var entity = _mapper.Map<LessonProgressEntity>(progress);
            return await _progressService.UpsertAsync(entity);
        }
    }
}
