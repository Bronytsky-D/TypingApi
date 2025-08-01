using AutoMapper;
using TypingWeb.Domain.Models.Entities;
using TypingWeb.Infrastructure.PostgreSQL.Models;

namespace TypingWeb.Infrastructure.PostgreSQL
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LessonProgress, LessonProgressEntity>().ReverseMap();
            CreateMap<Record, RecordEntity>().ReverseMap();
            CreateMap<RefreshToken, RefreshTokenEntity>().ReverseMap();
            CreateMap<User, UserEntity>().ReverseMap();
        }
    }
}
