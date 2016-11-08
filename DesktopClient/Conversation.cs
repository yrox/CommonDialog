using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BaseEntyties;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace DesktopClient
{
    public class Conversation
    {
        public string UserName { get; set; }
        public IHubProxy HubProxy { get; set; }
        const string ServerURI = @"http://localhost:8080/";
        public HubConnection Connection { get; set; }

        private async void ConnectAsync()
        {
            Connection = new HubConnection(ServerURI);
            Connection.Closed += ConnectionClosed;
            Connection.ConnectionSlow += ConnectionSlow;
            HubProxy = Connection.CreateHubProxy("ChatHub");
            
            try
            {
                await Connection.Start();
            }
            catch (HttpRequestException)
            {
                //Unable to connect to server
                return;
            }
        }

        private void ConnectionClosed()
        {
            //do smth
        }

        private void InitializeMethods()
        {
            //HubProxy.On<Message>("MessageSent",/*handle mes*/)
        }

        private void ConnectionSlow()
        {
            //notify
        }
    }
}
