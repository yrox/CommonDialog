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
            db = context;
        }

        private CommonDialogContext db;
        public IEnumerable<Contact> GetAll()
        {
            return db.Contacts;
        }
        public Contact Get(int id)
        {
            return db.Contacts.Find(id);
        }
        public IEnumerable<Contact> Find(Func<Contact, bool> predicate)
        {
            return db.Contacts.Where(predicate).ToList();
        }

        public void Add(Contact item)
        {
            db.Contacts.Attach(item);
            db.Entry(item).State = EntityState.Added;
        }

        public void Update(Contact item)
        {
            db.Entry(item).State = EntityState.Detached;
            db.UpdateGraph(item, map => map.OwnedEntity(x => x.GeneralContact));
        }

        public void Delete(Contact item)
        {
            db.Entry(item).State = EntityState.Deleted;
        }
    }
}

