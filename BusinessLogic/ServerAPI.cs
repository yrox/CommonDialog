using System.Collections.Generic;
using System.Linq;
using BaseEntyties;
using BusinessLogic.Accounts;
using BusinessLogic.Interfaces;
using BusinessLogic.Logic;

namespace BusinessLogic
{
    public class ServerAPI
    {
        public ServerAPI()
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

        //private void CreateAccs()
        //{
        //    _accs = new List<IAccount>();
        //    foreach (var acc in _dataHandler.GetDbAccounts())
        //    {
        //        if (acc.Type == "Vk")
        //            _accs.Add(new VkAccount(acc));
        //    }
        //}

        public void SaveGenContact(GeneralContact genContact)
        {
            _dataHandler.Save(genContact);
        }
        public void SaveAccount(Account acc)
        {
            _dataHandler.Save(acc);
        }
        public IEnumerable<Message> GetDbMessageHistory(GeneralContact genContact)
        {
            return _dataHandler.GetDbMessageHistory(genContact);
        }
        public IEnumerable<GeneralContact> GetDbGenContacts()
        {
            return _dataHandler.GetDbGenContacts();
        }
        public IEnumerable<Contact> GetDbContactsOf(GeneralContact genContact)
        {
            return _dataHandler.GetDbContactsOf(genContact);
        }

        public void SendMesage(Message message)
        {
            _dataHandler.Save(message);
            var accToSend = _accs.Single(a => a.AccountType == message.Type);
            _messaging.SendMessage(message, accToSend);
        }

        public IEnumerable<Message> LoadMessageHistoryOfContact(Contact contact)
        {
            var messages = _accs.Single(a => a.AccountType == contact.Type).GetMessagesByContact(contact).ToList();
            _dataHandler.SaveRange(messages);
            return messages;
        }
        public IEnumerable<Message> LoadMessageHistoryOfGenContact(GeneralContact genContact)
        {
            var messages = new List<Message>();
            foreach (var c in genContact.Contacts)
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
