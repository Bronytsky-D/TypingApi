using AutoMapper;
using TypingWebApi.Data.Models;
using TypingWebApi.Dtos;

namespace TypingWebApi.Profiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile() 
        {
            CreateMap<User, UserReadResponeDto>()
                .ForMember(dest => dest.ExperienceToNextLevel, opt => opt.MapFrom(src => 100 * (src.Level + 1)));
        }
    }
}
