using AutoMapper;
using TypingWebApi.Dtos;
using TypingWeb.Domain.Models.Entities;

namespace TypingWebApi.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<ProgressWriteRequestDto, LessonProgressEntity>();
        }
    }
}
