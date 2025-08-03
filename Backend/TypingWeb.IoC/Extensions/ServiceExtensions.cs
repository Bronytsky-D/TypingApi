using Microsoft.Extensions.DependencyInjection;
using TypingWeb.Domain.Abstractions.Services;
using TypingWeb.Infrastructure;
using TypingWeb.Infrastructure.PostgreSQL;
using TypingWeb.Infrastructure.PostgreSQL.Repositories;
using TypingWeb.Infrastructure.Repositories;
using TypingWeb.Service.Services;

namespace TypingWeb.Api.Extensions
{
    public static class ServiceExtensions
    {
        //public static void RegisterServices(this IServiceCollection services)
        //{
        //    //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        //    services.AddScoped<IUnitOfWork, UnitOfWork>();

        //    services.AddScoped<IRecordService, RecordService>();
        //    services.AddScoped<IUserService, UserService>();
        //    services.AddScoped<IUserAuthService, UserAuthService>();
        //    services.AddScoped<IUserGameService, UserGameService>();
        //    services.AddScoped<ITokenService, TokenService>();
        //    services.AddScoped<IProgressService, ProgressService>();


        //    services.AddScoped<IRecordRepository, RecordRepository>();
        //    services.AddScoped<ITokenRepository, TokenRepository>();
        //    services.AddScoped<IProgressRepository, ProgressRepository>();

        //}
    }
}
