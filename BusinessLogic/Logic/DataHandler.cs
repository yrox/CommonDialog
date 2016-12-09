using System.Collections.Generic;
using System.Linq;
using BaseEntyties;
using BusinessLogic.Mappers;
using DataLayer;

namespace BusinessLogic.Logic
{
    public class DataHandler
    {
        private DataHandler()
        {
            _data = new UnitOfWork();
            _extractor = new DataExtractor();
        }

        private UnitOfWork _data;
        private DataExtractor _extractor;

        public static DataHandler CreateDataHandler()
        {
            return new DataHandler();
        }

        public IEnumerable<Message> GetDbMessageHistory(MetaContact metaContact)
        {
            return _extractor.Extract(_data.Messages.Find(x => x.MetaContact.Id == metaContact.Id));
        }
        public IEnumerable<Message> GetDbMessageHistoryOfType(MetaContact metaContact, string type)
        {
            return _extractor.Extract(_data.MetaContacts.Get(metaContact.Id).Messages.Where(m => m.Type == type));
        }

        public IEnumerable<Contact> GetDbContactsOf(MetaContact metaContact)
        {
            _extractor.Extract(_data.Contacts.Find(x => x.MetaContact.Id == metaContact.Id));

            return _extractor.Extract(_data.Contacts.Find(x => x.MetaContact.Id == metaContact.Id));
        }
        public IEnumerable<MetaContact> GetDbmetaContacts()
        {
            var metas = _data.MetaContacts.GetAll().ToList();
            foreach (var item in metas)
            {
                var contacts = GetDbContactsOf(item);
                if (contacts != null)
                {
                    item.Contacts = new List<Contact>();
                    foreach (var contact in contacts)
                    {
                        item.Contacts.Add(contact);
                    }
                }

                var messages = GetDbMessageHistory(item);
                if (messages != null)
                {
                    item.Messages = new List<Message>();
                    foreach (var message in messages)
                    {
                        item.Messages.Add(message);
                    }
                }
                
            }
            return metas;
        }

        public IEnumerable<Account> GetDbAccounts()
        {
            return _data.Accounts.GetAll();
        } 
        

        public void Save(Message message)
        {
            using (var uw = new UnitOfWork())
            {
                uw.Messages.Add(message);
                uw.Save();
            }
            
        }
        public void SaveRange(IEnumerable<Message> messages)
        {
            using (var uw = new UnitOfWork())
            {
                uw.Messages.AddRange(messages);
                uw.Save();
            }
            
        }
        public void Save(Contact contact)
        {
            using (var uw = new UnitOfWork())
            {
                uw.Contacts.Add(contact);
                uw.Save();
            }
            
        }
        public void SaveRange(IEnumerable<Contact> contacts)
        {
            using (var uw = new UnitOfWork())
            {
                uw.Contacts.AddRange(contacts);
                uw.Save();
            }
            
        }
        public void Save(Account acc)
        {
            using (var uw = new UnitOfWork())
            {
                uw.Accounts.Add(acc);
                uw.Save();
            }
            
        }
        public void SaveRange(IEnumerable<Account> accounts)
        {
            using (var uw = new UnitOfWork())
            {
               uw.Accounts.AddRange(accounts);
                uw.Save();
            }
            
        }
        public void Save(MetaContact metaContact)
        {
            using (var uw = new UnitOfWork())
            {
                uw.MetaContacts.Add(metaContact);
                uw.Save();
            }
        }
        public void SaveRange(IEnumerable<MetaContact> metaContacts)
        {
            using (var uw = new UnitOfWork())
            {
                uw.MetaContacts.AddRange(metaContacts);
                uw.Save();
            }
        }

        public void Delete(MetaContact metaContact)
        {
            using (var uw = new UnitOfWork())
            {
                uw.MetaContacts.Delete(metaContact);
            }
        }
        public void DeleteRange(IEnumerable<MetaContact> metaContacts)
        {
            using (var uw = new UnitOfWork())
            {
                uw.MetaContacts.DeleteRange(metaContacts);
            }
        }

        public void SaveChanges()
        {
            _data.Save();
        }
    }
}
