using AutoMapper;
using TypingWebApi.Dtos;
using TypingWebApi.Data.Models;

namespace TypingWebApi.Profiles
{
    public class RecordMapperProfile : Profile
    {
        public RecordMapperProfile() 
        {
            CreateMap<RecordWriteRequestDto, Record>()
                .ForMember(dest => dest.DateRecord, opt => opt.MapFrom(src => DateTime.UtcNow)); 
        }
    }
}
