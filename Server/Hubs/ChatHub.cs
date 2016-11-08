using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using BusinessLogic;

namespace Server.Hubs
{
    public class ChatHub : Hub
    {
        public ChatHub(SomeClassWithSuitableName x)
        {
            this.x = x;
        }

        private SomeClassWithSuitableName x;

        public override Task OnConnected()
        {
            int hereIsSomeCode;
            // load changes
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled)
            {
                // We know that Stop() was called on the client,
                // save db context, close longpooling
            }
            else
            {
                // This server hasn't heard from the client in the last ~35 seconds.
                // If SignalR is behind a load balancer with scaleout configured, 
                // the client may still be connected to another SignalR server.
            }

            return base.OnDisconnected(stopCalled);
        }

      
    }
}
