using System.Collections.Generic;
using System.Linq;
using BaseEntyties;
using BusinessLogic.Accounts;
using BusinessLogic.Interfaces;
using BusinessLogic.Logic;

namespace BusinessLogic
{
    public class API
    {
        public API()
        {
            _dataHandler = DataHandler.CreateDataHandler();
            _contacts = new Contacts();
            _messaging = new Messaging();
            _accs = new List<IAccount>();
            _accs.Add(new VkAccount());
           // CreateAccs();
            
        }

        private IList<IAccount> _accs;
        private Messaging _messaging;
        private Contacts _contacts;
        private DataHandler _dataHandler;

        private void CreateAccs()
        {
            _accs = new List<IAccount>();
            foreach (var acc in _dataHandler.GetDbAccounts())
            {
                if (acc.Type == "Vk")
                    _accs.Add(new VkAccount(acc));
            }
        }
        public void Authorise()
        {
            
        }

        public void SavemetaContact(MetaContact metaContact)
        {
            _dataHandler.Save(metaContact);
        }
        public void SaveAccount(Account acc)
        {
            _dataHandler.Save(acc);
        }
        public IEnumerable<Message> GetDbMessageHistory(MetaContact metaContact)
        {
            return _dataHandler.GetDbMessageHistory(metaContact);
        }
        public IEnumerable<MetaContact> GetDbmetaContacts()
        {
            return _dataHandler.GetDbmetaContacts();
        }
        public IEnumerable<Contact> GetDbContactsOf(MetaContact metaContact)
        {
            return _dataHandler.GetDbContactsOf(metaContact);
        }

        public void SendMesage(Message message)
        {
            _dataHandler.Save(message);
            var accToSend = _accs.Single(a => a.AccountType == message.Type);
            _messaging.SendMessage(message, accToSend);
        }//initalize datetime

        public IEnumerable<Message> LoadMessageHistoryOfContact(Contact contact)
        {
            var messages = _accs.Single(a => a.AccountType == contact.Type).GetMessagesByContact(contact).ToList();
            _dataHandler.SaveRange(messages);
            return messages;
        }
        public IEnumerable<Message> LoadMessageHistoryOfMetaContact(MetaContact metaContact)
        {
            var messages = new List<Message>();
            foreach (var c in metaContact.Contacts)
            {
                messages.AddRange(LoadMessageHistoryOfContact(c));
            }
            _dataHandler.SaveRange(messages);
            return messages.OrderBy(m => m.DateTime); 
        }

        public IEnumerable<Contact> LoadContactsOfType(string type)
        {
            return _accs.Single(a => a.AccountType == type).GetAllContacts();
        }
        public IEnumerable<Contact> LoadAllContacts()
        {
            var cont = new List<Contact>();
            foreach (var acc in _accs)
            {
                cont.AddRange(LoadContactsOfType(acc.AccountType));
            }
            return cont;
        }
        public Contact GetContact(string type, int id)
        {
            return _contacts.GetContact(_accs.Single(c => c.AccountType == type), id);
        }
        public Contact GetContact(string type, string nameOrPhoneNumber)
        {
            return _contacts.GetContact(_accs.Single(c => c.AccountType == type), nameOrPhoneNumber);
        }
    }
}
