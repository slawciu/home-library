using System.Threading.Tasks;
using System.Web.Http;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using HomeLib.Api.Hubs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;


namespace HomeLibraryWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = InversionOfControlConfiguration.Container;
            
            var signalrResolver = new AutofacDependencyResolver(container);
            var webapiResolver = new AutofacWebApiDependencyResolver(container);
            
            GlobalHost.DependencyResolver = signalrResolver;

            var webApiconfig = new HttpConfiguration { DependencyResolver = webapiResolver };

            // Json settings
            webApiconfig.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            webApiconfig.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            webApiconfig.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

            webApiconfig.MapHttpAttributeRoutes();

            webApiconfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional});

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login"),
            });

            app.UseWebApi(webApiconfig);

            app.UseAutofacMiddleware(container);

            var hubConfiguration = new HubConfiguration
            {
                Resolver = signalrResolver
            };

            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR("/signalr", hubConfiguration);

            
        }
    }
}
