﻿using System;
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
            var c = new HubConfiguration();
            c.EnableDetailedErrors = true;
            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(60);
            GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(20);
            GlobalHost.Configuration.TransportConnectTimeout = TimeSpan.FromSeconds(10);
            GlobalHost.DependencyResolver.Register(
            typeof(ChatHub),
            () => new ChatHub(api));

            app.MapSignalR("/signalr", new HubConfiguration());
        }
    }
}
