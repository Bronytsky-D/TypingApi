using AutoMapper;

using TypingWeb.Auth.Api.Dtos;

namespace TypingWeb.Auth.Api.Profiles
{
    public class UserRegistrMappingProfile : Profile
    {
        public UserRegistrMappingProfile() 
        {
            //CreateMap<RegisterRequestDto, User>()
            //    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
