using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TypingWeb.Infrastructure;
using TypingWeb.Infrastructure.PostgreSQL;
using TypingWeb.Infrastructure.PostgreSQL.Models;
using TypingWeb.Infrastructure.PostgreSQL.Repositories;
using TypingWeb.Infrastructure.Repositories;

namespace TypingWeb.IoC.Containers
{
    internal static class InfrasrtuctureContainer
    {
        public static void Register(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            //services.AddDbContext<ApplicationContext>(option =>
            //    option.UseNpgsql(configurationManager.GetConnectionString("DefaultConnection")));

            //services.AddIdentity<User, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationContext>()
            //    .AddDefaultTokenProviders();
            //here
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IProgressRepository, ProgressRepository>();

        }
    }
}
