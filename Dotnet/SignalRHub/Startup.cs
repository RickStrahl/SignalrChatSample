using System.Web.Routing;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using SignalRHub.WebStoreNotificationHub;
using Westwind.Web;

[assembly: OwinStartup(typeof(SignalRHub.Startup))]

namespace SignalRHub
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            // from Westwind.Web - hook up route mapping for Route Urls
            CallbackHandlerRouteHandler.RegisterRoutes<WebStoreOrderService>(RouteTable.Routes);

            app.UseCors(CorsOptions.AllowAll);

            var signalRConfig = new Microsoft.AspNet.SignalR.HubConfiguration()
            {
                EnableDetailedErrors = true,
                EnableJavaScriptProxies = true,                
            };
            app.MapSignalR("/signalr",signalRConfig);
        }
    }
}
