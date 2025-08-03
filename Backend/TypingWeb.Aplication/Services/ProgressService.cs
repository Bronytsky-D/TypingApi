using TypingWeb.Common;
using TypingWeb.Domain;
using TypingWeb.Domain.Abstractions.Services;
using TypingWeb.Domain.Models.Entities;
using TypingWeb.Infrastructure;

namespace TypingWeb.Service.Services
{
    public class ProgressService: IProgressService
    {
        private readonly IUnitOfWork _unitOfWork;
        const int MAX_WPM = 50;

        public ProgressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IExecutionResponse> GetByUserAndLessonIdAsync(string userId, int lessonId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return ExecutionResponse.Failure("userId empty");

            var lp = await _unitOfWork.Progress.GetByUserAndLessonAsync(userId, lessonId);
            return lp != null
                ? ExecutionResponse.Successful(lp)
                : ExecutionResponse.Failure("Lesson progress not found");
        }
        public async Task<IExecutionResponse> GetByUserIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return ExecutionResponse.Failure("userId empty");

            var progresses = await _unitOfWork.Progress.GetByUserAsync(userId)
                            ?? new List<LessonProgressEntity>();

            return ExecutionResponse.Successful(progresses);
        }

        public async Task<IExecutionResponse> UpsertAsync(LessonProgressEntity dto)
        {
            if (dto == null)
                return ExecutionResponse.Failure("Input data is required");

            if (string.IsNullOrWhiteSpace(dto.UserId) || dto.LessonId <= 0)
                return ExecutionResponse.Failure("Invalid userId or lessonId");

            var entry = await _unitOfWork.Progress.GetByUserAndLessonAsync(dto.UserId, dto.LessonId);
            var pct = Math.Min(Math.Round(dto.BestWpm / (double)MAX_WPM * 100), 100);

            if (entry == null)
            {
                dto.ProgressPercent = pct;
                dto.LastUpdated = DateTime.UtcNow;
                await _unitOfWork.Progress.AddAsync(dto);
            }
            else if (dto.BestWpm > entry.BestWpm)
            {
                entry.BestWpm = dto.BestWpm;
                entry.BestRaw = dto.BestRaw;
                entry.BestAccuracy = dto.BestAccuracy;
                entry.ProgressPercent = pct;
                entry.LastUpdated = DateTime.UtcNow;
                await _unitOfWork.Progress.UpdateAsync(entry);
            }

            try
            {
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                // Логування винятку ex
                return ExecutionResponse.Failure("Error saving progress");
            }

            return ExecutionResponse.Successful(dto);
        }

    }
}
