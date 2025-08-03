using AutoMapper;
using TypingWeb.Domain.Models.Entities;
using TypingWeb.Infrastructure.PostgreSQL.Models;

namespace TypingWeb.Infrastructure.PostgreSQL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LessonProgress, LessonProgressEntity>();
            CreateMap<LessonProgressEntity, LessonProgress>();
            CreateMap<Record, RecordEntity>()
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore());
            CreateMap<RefreshToken, RefreshTokenEntity>().ReverseMap();
            CreateMap<User, UserEntity>().ReverseMap();
        }
    }
}
