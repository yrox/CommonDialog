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

        public IEnumerable<Message> GetDbMessageHistory(MetaContact metaContact)
        {
            return _data.MetaContacts.Get(metaContact.Id).Messages;
        }
        public IEnumerable<Message> GetDbMessageHistoryOfType(MetaContact metaContact, string type)
        {
            return _data.MetaContacts.Get(metaContact.Id).Messages.Where(m => m.Type == type);
        }

        public IEnumerable<Contact> GetDbContactsOf(MetaContact metaContact)
        {
            return _data.MetaContacts.Get(metaContact.Id).Contacts;
        }
        public IEnumerable<MetaContact> GetDbmetaContacts()
        {
            return _data.MetaContacts.GetAll();
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
        public void Save(MetaContact metaContact)
        {
            _data.MetaContacts.Add(metaContact);
        }
        public void SaveRange(IEnumerable<MetaContact> metaContacts)
        {
            _data.MetaContacts.AddRange(metaContacts);
        }

        public void SaveChanges()
        {
            _data.Save();
        }
    }
}
