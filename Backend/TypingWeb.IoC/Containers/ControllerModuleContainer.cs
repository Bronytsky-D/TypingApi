using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TypingWeb.Aplication.UseCases.Validations;

namespace TypingWeb.IoC.Containers
{
    internal static class ControllerModuleContainer
    {
        public static void Register(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var JWTSetting = configurationManager.GetSection("JWTSetting");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                    {
                        opt.SaveToken = true;
                        opt.RequireHttpsMetadata = false;
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidAudience = JWTSetting["ValidAudience"],
                            ValidIssuer = JWTSetting["ValidIssuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSetting["SecurityKey"]))
                        };
                    });

            // ? CORS ДОДАНИЙ ТУТ — ДО builder.Build()
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });


            services.AddAutoMapper(
                typeof(TypingWeb.Aplication.UseCases.Profiles.AutoMapperProfile).Assembly,
                typeof(TypingWeb.Aplication.UseCases.Profiles.RecordMapperProfile).Assembly,
                typeof(TypingWeb.Infrastructure.PostgreSQL.AutoMapperProfile).Assembly
            );

            services.AddValidatorsFromAssembly(
                 typeof(ProgressWriteRequestDtoValidator).Assembly,
                 includeInternalTypes: true
            );

            services.AddValidatorsFromAssembly(
                typeof(RecordWriteRequestDtoValidator).Assembly,
                includeInternalTypes: true
            );
            services.AddAuthorization();

        }
    }
}
