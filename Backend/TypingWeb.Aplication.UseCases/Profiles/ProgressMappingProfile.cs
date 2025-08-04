using AutoMapper;
using TypingWebApi.Dtos;
using TypingWeb.Domain.Models.Entities;
using TypingWeb.Common.DTOs;

namespace TypingWeb.Aplication.UseCases.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<ProgressWriteRequestDto, LessonProgressEntity>();
        }
    }
}
