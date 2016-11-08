using System.Collections.Generic;
using System.Linq;
using BaseEntyties;
using BusinessLogic.Data;
using BusinessLogic.Interfaces;
using BusinessLogic.Logic;
using DataLayer;

namespace BusinessLogic
{
    public class SomeClassWithSuitableName
    {
        public SomeClassWithSuitableName(IEnumerable<IAccount> accs, DataHandler dataHandler)
        {
            _accs = accs;
            _dataHandler = dataHandler;
        }

        private IEnumerable<IAccount> _accs;
        private Messaging _messaging;
        private Contacts _contacts;
        private DataHandler _dataHandler;

        public void SendMesage(Message message)
        {
            var accToSend = _accs.Single(a => a.AccountType == message.Type);
            _messaging.SendMessage(message, accToSend);
        }
        
        public IEnumerable<Message> LoadMessageHistoryOfContact(Contact contact)
        {
            return _accs.Single(a => a.AccountType == contact.Type).GetMessagesByContact(contact);
        }
        public IEnumerable<Message> LoadMessageHistoryOfGenContact(GeneralContact genContact)
        {
            var messages = new List<Message>();
            foreach (var c in genContact.Contacts)
            {
                messages.AddRange(LoadMessageHistoryOfContact(c));
            }
            return messages.OrderBy(m => m.DateTime); 
        }


        public IEnumerable<Contact> LosdAllContactsOfType(string type)
        {
            return _accs.Single(a => a.AccountType == type).GetAllContacts();
        }
        public IEnumerable<Contact> LosdAllContacts()
        {
            var cont = new List<Contact>();
            foreach (var acc in _accs)
            {
                cont.AddRange(LosdAllContactsOfType(acc.AccountType));
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
