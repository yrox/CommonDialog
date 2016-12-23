using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BaseEntyties;
using RefactorThis.GraphDiff;

namespace DataLayer.Repositories
{
    public class ContactRepository : IRepository<Contact>
    {
        public ContactRepository(CommonDialogContext context)
        {
            _db = context;
            //_db.Contacts.Load();
        }

        private CommonDialogContext _db;

        public bool Contains(Contact item)
        {
            return _db.Contacts.Any(
                     c => c.MetaContactId == item.MetaContactId &&
                     c.ContactIdentifier == item.ContactIdentifier &&
                     c.Type == item.Type);
        }
        public IEnumerable<Contact> GetAll()
        {
            return _db.Contacts;
        }
        public Contact Get(int id)
        {
            return _db.Contacts.Find(id);
        }
        public IEnumerable<Contact> Find(Func<Contact, bool> predicate)
        {
            return _db.Contacts.Where(predicate).ToList();
        }

        public void Add(Contact item)
        {
            if (!Contains(item))
            {
                _db.Contacts.Add(item);
            }
            else
            {
                Update(item);
            }
        }
        public void AddRange(IEnumerable<Contact> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public void Update(Contact item)
        {
            _db.Entry(item).State = EntityState.Detached;
            _db.UpdateGraph(item, map => map.OwnedEntity(x => x.MetaContactId));
        }

        public void Delete(Contact item)
        {
            _db.Contacts.Remove(item);
        }

        public void DeleteRange(IEnumerable<Contact> contacts)
        {
            _db.Contacts.RemoveRange(contacts);
        }
    }
}

