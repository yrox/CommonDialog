using System.Collections.Generic;
using System.Threading.Tasks;
using BaseEntyties;
using Microsoft.AspNet.SignalR;
using BusinessLogic;

namespace Server.Hubs
{
    public class ChatHub : Hub
    {
        public ChatHub(ServerAPI serverApi)
        {
            _serverApi = serverApi;
        }

        private ServerAPI _serverApi;

        public void MessageRecived(Message message)
        {
            Clients.Client(Context.ConnectionId).MessageRecived(message);
        }

        public void SendMessage(Message message)
        {
            _serverApi.SendMesage(message);
        }

        public void SaveAccount(Account acc)
        {
            _serverApi.SaveAccount(acc);
        }
        public void SaveGenContact(GeneralContact genContact)
        {
            _serverApi.SaveGenContact(genContact);
        }
        public IEnumerable<Message> GetDbMessageHistory(GeneralContact genContact)
        {
            return _serverApi.GetDbMessageHistory(genContact);
        }
        public IEnumerable<Contact> GetDbContactsOf(GeneralContact genContact)
        {
            return _serverApi.GetDbContactsOf(genContact);
        }
        public IEnumerable<GeneralContact> GetDbGenContacts()
        {
            return _serverApi.GetDbGenContacts();
        }

        public IEnumerable<Message> LoadMessageHistory(GeneralContact genContact)
        {
            return _serverApi.LoadMessageHistoryOfGenContact(genContact);
        }
        public IEnumerable<Message> LoadMessageHistory(Contact contact)
        {
            return _serverApi.LoadMessageHistoryOfContact(contact);
        }

        public IEnumerable<Contact> LoadContactsOfType(string type)
        {
            return _serverApi.LoadContactsOfType(type);
        }
        public IEnumerable<Contact> LoadAllContacts()
        {
            return _serverApi.LoadAllContacts();
        } 
        public Contact GetContact(string type, int id)
        {
            return _serverApi.GetContact(type, id);
        }
        public Contact GetContact(string type, string nameOrPhneNumber)
        {
            return _serverApi.GetContact(type, nameOrPhneNumber);
        }

        public override Task OnConnected()
        {
            Groups.Add(Context.ConnectionId, Context.User.Identity.Name);
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
