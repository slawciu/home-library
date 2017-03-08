using System.Reflection;
using Autofac;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using HomeLib.Api.Hubs;
using Newtonsoft.Json;

namespace HomeLibraryWeb
{
    public class InversionOfControlConfiguration
    {
        private static IContainer _container;

        public static IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = BuildContainer();
                }

                return _container;
            }
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterHubs(typeof(BooksHub).Assembly);

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var serializer = JsonSerializer.CreateDefault();
            builder.Register(s => serializer).As<JsonSerializer>();
            
            return builder.Build();
        }
    }
}