using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TypingWeb.Infrastructure;
using TypingWeb.Infrastructure.PostgreSQL;
using TypingWeb.Infrastructure.PostgreSQL.Repositories;
using TypingWeb.Infrastructure.Repositories;

namespace TypingWeb.IoC.Containers
{
    internal static class InfrasrtuctureContainer
    {
        public static void Register(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddDbContext<ApplicationContext>(option =>
                option.UseNpgsql(configurationManager.GetConnectionString("DefaultConnection")));

            //here
            services.AddScoped<IProgressRepository, ProgressRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
