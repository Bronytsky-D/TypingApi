using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TypingWeb.Auth.Infrastructure.PostgreSQL.Models;
using TypingWeb.Auth.Infrastucture.PostgreSQL;

namespace TypingWeb.Auth.IoC.Containers
{
    internal static class InfrasrtuctureContainer
    {
        public static void Register(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddDbContext<AuthDbContext>(option =>
                option.UseNpgsql(configurationManager.GetConnectionString("DefaultConnection")));
           
            services.AddIdentity<UserIdentity, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            // Add other necessary services here
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddScoped<IRecordRepository, RecordRepository>();
            //services.AddScoped<ITokenRepository, TokenRepository>();
            //services.AddScoped<IProgressRepository, ProgressRepository>();
        }
    }
}
