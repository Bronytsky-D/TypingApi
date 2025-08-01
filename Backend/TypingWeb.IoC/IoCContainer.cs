using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TypingWeb.IoC.Containers;

namespace TypingWeb.IoC
{
    public static class IoCContainer
    {
        public static void RegisterServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            InfrasrtuctureContainer.Register(services, configurationManager);
            ApplicationContainer.Register(services, configurationManager);
            //BaseModule.Register(services);
            //MediatRModule.Register(services);
            //ControllerModule.Register(services);
        }
    }
}
