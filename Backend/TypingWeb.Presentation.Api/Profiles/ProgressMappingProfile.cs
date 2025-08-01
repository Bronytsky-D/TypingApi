using AutoMapper;
using TypingWebApi.Dtos;
using TypingWeb.Infrastructure.PostgreSQL.Models;

namespace TypingWebApi.Profiles
{
    public class ProgressMappingProfile : Profile
    {
        public ProgressMappingProfile() 
        {
            CreateMap<ProgressWriteRequestDto, LessonProgress>();
        }
    }
}
