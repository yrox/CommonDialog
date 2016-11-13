using System.Collections.Generic;
using System.Linq;
using BaseEntyties;
using DataLayer;

namespace BusinessLogic.Logic
{
    public class DataHandler
    {
        private DataHandler()
        {
            _data = new UnitOfWork();
        }

        private UnitOfWork _data;

        public static DataHandler CreateDataHandler()
        {
            return new DataHandler();
        }

        public IEnumerable<Message> GetDbMessageHistory(GeneralContact genContact)
        {
            return _data.GeneralContacts.Get(genContact.Id).Messages;
        }
        public IEnumerable<Message> GetDbMessageHistoryOfType(GeneralContact genContact, string type)
        {
            return _data.GeneralContacts.Get(genContact.Id).Messages.Where(m => m.Type == type);
        }

        public IEnumerable<Contact> GetDbContactsOf(GeneralContact genContact)
        {
            return _data.GeneralContacts.Get(genContact.Id).Contacts;
        }
        public IEnumerable<GeneralContact> GetDbGenContacts()
        {
            return _data.GeneralContacts.GetAll();
        }

        public IEnumerable<Account> GetDbAccounts()
        {
            return _data.Accounts.GetAll();
        } 
        

        public void Save(Message message)
        {
            _data.Messages.Add(message);
        }
        public void SaveRange(IEnumerable<Message> messages)
        {
            _data.Messages.AddRange(messages);
        }
        public void Save(Contact contact)
        {
            _data.Contacts.Add(contact);
        }
        public void SaveRange(IEnumerable<Contact> contacts)
        {
            _data.Contacts.AddRange(contacts);
        }
        public void Save(Account acc)
        {
            _data.Accounts.Add(acc);
        }
        public void SaveRange(IEnumerable<Account> accounts)
        {
            _data.Accounts.AddRange(accounts);
        }
        public void Save(GeneralContact genContact)
        {
            _data.GeneralContacts.Add(genContact);
        }
        public void SaveRange(IEnumerable<GeneralContact> genContacts)
        {
            _data.GeneralContacts.AddRange(genContacts);
        }

        public void SaveChanges()
        {
            _data.Save();
        }
    }
}
