using System;
using System.Collections.Generic;
using System.Linq;
using BaseEntyties;
using RefactorThis.GraphDiff;
using System.Data.Entity;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class MetaContactRepository : IRepository<MetaContact>
    {
        public MetaContactRepository(CommonDialogContext context)
        {
            _db = context;
            //_db.Contacts.Load();
        }

        private CommonDialogContext _db;

        public bool Contains(MetaContact metaContact)
        {
            return _db.MetaContacts.Any(g => g.Id == metaContact.Id || g.Id == 0);
        }
        public IEnumerable<MetaContact> GetAll()
        {
            return _db.MetaContacts;
        }
        public MetaContact Get(int id)
        {
            return _db.MetaContacts.Find(id);
        }
        public IEnumerable<MetaContact> Find(Func<MetaContact, bool> predicate)
        {
            return _db.MetaContacts.Where(predicate).ToList();
        }

        public void Add(MetaContact item)
        {
            if (!Contains(item))
            {
                _db.MetaContacts.Add(item);
            }
            else
            {
                Update(item);
            }
        }

        public void AddRange(IEnumerable<MetaContact> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public void Update(MetaContact item)
        {
            _db.Entry(item).State = EntityState.Detached;
            _db.UpdateGraph(item, map => map.OwnedCollection(x => x.Contacts));
            _db.UpdateGraph(item, map => map.OwnedCollection(x => x.Messages));
        }

        public void Delete(MetaContact item)
        {
            //var contToDelete = _db.Contacts.Where(x => x.MetaContactId == item.Id).ToList();
            //var mesToDelete = _db.Messages.Where(x => x.MetaContactId == item.Id).ToList();
            item = _db.MetaContacts.Include(x => x.Contacts).Include(x => x.Messages).First(x => x.Id == item.Id);
            //_db.Contacts.RemoveRange(contToDelete);
            //_db.Messages.RemoveRange(mesToDelete);
            _db.MetaContacts.Remove(item);
        }

        public void DeleteRange(IEnumerable<MetaContact> contacts)
        {
            foreach (var item in contacts)
            {
                Delete(item);
            }
        }
    }
}
