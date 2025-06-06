﻿using Domain;
using Domain.Models;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingWebApi.Domains.Models.Types;

namespace Service.Services
{
    public class ProgressService: IProgressService
    {
        private readonly IUnitOfWork _unitOfWork;
        const int MAX_WPM = 50;

        public ProgressService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

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

            // Припускаємо, що _unitOfWork.Progress.GetByUserAsync повертає
            // саме List<LessonProgress> або null, якщо записів немає.
            var progresses = await _unitOfWork.Progress.GetByUserAsync(userId)
                            ?? new List<LessonProgress>();

            // Завжди повертаємо успішний результат із фактичним списком
            return ExecutionResponse.Successful(progresses);
        }


        public async Task<IExecutionResponse> UpsertAsync(LessonProgress dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserId) || dto.LessonId <= 0)
                return ExecutionResponse.Failure("Invalid input");

            var entry = await _unitOfWork.Progress.GetByUserAndLessonAsync(dto.UserId, dto.LessonId);
            var pct = Math.Min(Math.Round(dto.BestWpm / (double)MAX_WPM * 100), 100);

            if (entry == null)
            {
                dto.ProgressPercent = pct;
                await _unitOfWork.Progress.AddAsync(dto);
            }
            else if (dto.BestWpm > entry.BestWpm)
            {
                entry.BestWpm = dto.BestWpm;
                entry.BestRaw = dto.BestRaw;
                entry.BestAccuracy = dto.BestAccuracy;
                entry.ProgressPercent = pct;
                entry.LastUpdated = DateTime.UtcNow;
                _unitOfWork.Progress.Update(entry);
            }

            await _unitOfWork.CommitAsync();
            return ExecutionResponse.Successful(dto);
        }
    }
}
