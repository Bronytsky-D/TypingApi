using Domain;
using Domain.Repositories;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Repositories;
using Service.Services;
using TypingWebApi.Data;
using TypingWebApi.Service;

namespace TypingWebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRecordService, RecordService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProgressService, ProgressService>();


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IProgressRepository, ProgressRepository>();

        }
    }
}
