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
    public class ServerInteraсtion
    {
        private void InitializeMethods()
        {
            Connection.Closed += ConnectionClosed;
            Connection.ConnectionSlow += ConnectionSlow;
            HubProxy.On<Message>("MessageRecived", message =>
            {
                //notify user
            });
        }

        private async void ConnectAsync()
        {
            Connection = new HubConnection(ServerURI);
            InitializeMethods();
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
        private void ConnectionSlow()
        {
            //notify
        }
 
        public string UserName { get; set; }
        public IHubProxy HubProxy { get; set; }
        const string ServerURI = @"http://localhost:8080/";
        public HubConnection Connection { get; set; }

        public void SendMessage(Message message)
        {
            HubProxy.Invoke("SendMessage", message);
        }

        public void SaveAccount(Account account)
        {
            HubProxy.Invoke("SaveAccount", account);
        }
        public void SaveGenContact(GeneralContact genContact)
        {
            HubProxy.Invoke("SaveGenContact", genContact);
        }
        public async Task<IEnumerable<Message>> GetDbMessageHistory(GeneralContact genContact)
        {
            var result = await  Task.FromResult(HubProxy.Invoke<IEnumerable<Message>>("GetDbMessaeHistory", genContact));
            return await result;
        }
        public async Task<IEnumerable<Contact>> GetDbContactsOf(GeneralContact genContact)
        {
            var result = await Task.FromResult(HubProxy.Invoke<IEnumerable<Contact>>("GetDbContactsOf", genContact));
            return await result;
        }
        public async Task<IEnumerable<GeneralContact>> GetDbGenContacts()
        {
            var result = await Task.FromResult(HubProxy.Invoke<IEnumerable<GeneralContact>>("GetDbGenContacts"));
            return await result;
        }

        public async Task<IEnumerable<Message>> LoadMessageHistory(GeneralContact genContact)
        {
            var result = await Task.FromResult(HubProxy.Invoke<IEnumerable<Message>>("LoadMessageHistory", genContact));
            return await result;
        }
        public async Task<IEnumerable<Message>> LoadMessageHistory(Contact сontact)
        {
            var result = await Task.FromResult(HubProxy.Invoke<IEnumerable<Message>>("LoadMessageHistory", сontact));
            return await result;
        }
        public async Task<IEnumerable<Contact>> LoadContactsOfType(string type)
        {
            var result = await Task.FromResult(HubProxy.Invoke<IEnumerable<Contact>>("LoadContactsOfType", type));
            return await result;
        }
        public async Task<IEnumerable<Contact>> LoadAllContacts()
        {
            var result = await Task.FromResult(HubProxy.Invoke<IEnumerable<Contact>>("LoadAllContacts"));
            return await result;
        }
        public async Task<Contact> GetContact(string type, int id)
        {
            var result = await Task.FromResult(HubProxy.Invoke<Contact>("GetCntact", type, id));
            return await result;
        }
        public async Task<Contact> GetContact(string type, string nameOrPhoneNumber)
        {
            var result = await Task.FromResult(HubProxy.Invoke<Contact>("GetCntact", type, nameOrPhoneNumber));
            return await result;
        }
    }
}
