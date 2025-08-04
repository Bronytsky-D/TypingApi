using AutoMapper;
using TypingWeb.Common.DTOs;
using TypingWeb.Domain.Models.Entities;
using TypingWebApi.Dtos;


namespace TypingWeb.Aplication.UseCases.Profiles
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
