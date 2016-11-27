using BusinessLogic;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using Server.Hubs;

[assembly: OwinStartup(typeof(Server.Startup))]

namespace Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var api = new API();
            GlobalHost.DependencyResolver.Register(
            typeof(ChatHub),
            () => new ChatHub(api));

            app.MapSignalR("/signalr", new HubConfiguration());
        }
    }
}
