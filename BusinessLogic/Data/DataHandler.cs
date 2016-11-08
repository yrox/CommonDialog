using System.Collections.Generic;
using System.Linq;
using BaseEntyties;
using DataLayer;

namespace BusinessLogic.Data
{
    public class DataHandler
    {
        public DataHandler(UnitOfWork data)
        {
            _data = data;
        }

        private UnitOfWork _data;

        public IEnumerable<Message> GetDbMessageHistory(GeneralContact genContact)
        {
            return _data.GeneralContacts.Get(genContact.Id).Messages;
        }
        public IEnumerable<Message> GetDbMessageHistoryOfType(GeneralContact genContact, string type)
        {
            return _data.GeneralContacts.Get(genContact.Id).Messages.Where(m => m.Type == type);
        }

        public IEnumerable<Contact> GetAllDbContacts(GeneralContact genContact)
        {
            return _data.GeneralContacts.Get(genContact.Id).Contacts;
        }

        private void Update(GeneralContact genContact)
        {
            _data.GeneralContacts.Update(genContact);
            SaveChanges();
        }
        private void Update(Contact сontact)
        {
            _data.Contacts.Update(сontact);
            SaveChanges();
        }
        private void Update(Message message)
        {
            _data.Messages.Update(message);
            SaveChanges();
        }

        public void Save(Message message)
        {
            if (!_data.Messages.Contains(message))
            {
                _data.Messages.Add(message);
            }
            Update(message);
        }
        public void Save(Contact contact)
        {
            _data.Contacts.Add(contact);
            Update(contact);
        }
        public void Save(Account acc)
        {
            _data.Accounts.Add(acc);
        }
        public void Save(GeneralContact genContact)
        {
            _data.GeneralContacts.Add(genContact);
        }

        public void SaveChanges()
        {
            _data.Save();
        }
    }
}
