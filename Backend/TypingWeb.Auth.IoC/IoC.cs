using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TypingWeb.Auth.IoC.Containers;

namespace TypingWeb.Auth.IoC
{
    public static class IoCContainer
    {
        public static void RegisterServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            InfrasrtuctureContainer.Register(services, configurationManager);
            ApplicationContainer.Register(services, configurationManager);
            ControllerModuleContainer.Register(services, configurationManager);
        }
    }
}
