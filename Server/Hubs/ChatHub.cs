using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseEntyties;
using Microsoft.AspNet.SignalR;
using BusinessLogic;
using VkNet.Exception;

namespace Server.Hubs
{
    public class ChatHub : Hub
    {
        public ChatHub(API serverApi)
        {
            _serverApi = serverApi;
        }

        private API _serverApi;

        private void CaptchaHandler(CaptchaNeededException cEx)
        {
            Clients.All.RecognizeCaptcha(cEx.Img, cEx.Sid);
        }

        public void Authorize(string code)
        {
            try
            {
                _serverApi.Authorize(code);
            }

            catch (CaptchaNeededException cEx)
            {
                CaptchaHandler(cEx);
            }

        }

        public void SendCaptcha(string captcha, long sid)
        {
            _serverApi.Authorize(captcha, sid);
        }

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

        public void SaveMetaContact(MetaContact metaContact)
        {
            _serverApi.SavemetaContact(metaContact);
        }

        public IEnumerable<Message> GetDbMessageHistory(MetaContact metaContact)
        {
            return _serverApi.GetDbMessageHistory(metaContact).ToList();
        }

        public IEnumerable<Contact> GetDbContactsOf(MetaContact metaContact)
        {
            return _serverApi.GetDbContactsOf(metaContact);
        }

        public IEnumerable<MetaContact> GetDbMetaContacts()
        {
            return _serverApi.GetDbMetaContacts().ToList();

        }

        public IEnumerable<Message> LoadMetaMessageHistory(MetaContact metaContact)
        {
            return _serverApi.LoadMessageHistoryOfMetaContact(metaContact);
        }

        public IEnumerable<Message> LoadContactMessageHistory(Contact contact)
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

        public Contact GetContactById(string type, int id)
        {
            return _serverApi.GetContact(type, id);
        }

        public Contact GetContact(string type, string nameOrPhneNumber)
        {
            return _serverApi.GetContact(type, nameOrPhneNumber);
        }

        public override Task OnConnected()
        {
            //todo load changes
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
