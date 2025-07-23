using AutoMapper;
using TypingWebApi.Dtos;
using TypingWebApi.Data.Models;
using Domain.Models;

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
