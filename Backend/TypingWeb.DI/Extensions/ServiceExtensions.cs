using Domain.Models.Types;
using Domain.Repositories;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository.UnitOfWork;
using Service.Services;
using TypingWebApi.Service;

namespace TypingWeb.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRecordService, RecordService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserAuthService, UserAuthService>();
            services.AddScoped<IUserGameService, UserGameService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProgressService, ProgressService>();


            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IProgressRepository, ProgressRepository>();

        }
    }
}
