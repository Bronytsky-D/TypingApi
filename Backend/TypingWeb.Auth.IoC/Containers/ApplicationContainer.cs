

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TypingWeb.Auth.IoC.Containers
{
    internal static class ApplicationContainer
    {
        public static void Register(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            //services.AddScoped<IRecordService, RecordService>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IUserAuthService, UserAuthService>();
            //services.AddScoped<IUserGameService, UserGameService>();
            //services.AddScoped<ITokenService, TokenService>();
            //services.AddScoped<IProgressService, ProgressService>();

            //services.AddScoped<IGetRecordUseCase, GetRecordUseCase>();
            //services.AddScoped<IAddRecordUseCase, AddRecordUseCase>();
            //services.AddScoped<IGetUserUseCase, GetUserUseCase>();
            //services.AddScoped<IGetProgressUseCase, GetProgressUseCase>();
            //services.AddScoped<IGetProgressByUserAndLessonUseCase, GetProgressByUserAndLessonUseCase>();
            //services.AddScoped<IUpserProgressUseCase, UpserProgressUseCase>();
        }
    }
}
