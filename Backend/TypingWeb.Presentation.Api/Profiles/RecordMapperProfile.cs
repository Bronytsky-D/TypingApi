using AutoMapper;
using TypingWeb.Domain.Models.Entities;
using TypingWebApi.Dtos;


namespace TypingWebApi.Profiles
{
    public class RecordMapperProfile : Profile
    {
        public RecordMapperProfile() 
        {
            CreateMap<RecordWriteRequestDto, RecordEntity>()
                .ForMember(dest => dest.DateRecord, opt => opt.MapFrom(src => DateTime.UtcNow)); 
        }
    }
}
