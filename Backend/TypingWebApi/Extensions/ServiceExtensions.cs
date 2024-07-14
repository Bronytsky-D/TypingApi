using TypingWebApi.Data;
using TypingWebApi.Service;

namespace TypingWebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddScoped<IRecordService, RecordService>();
        }
    }
}
