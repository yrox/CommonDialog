using System;
using System.Collections.Generic;
using System.Data;
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
            _db.Entry(item).State = EntityState.Deleted;
        }
    }
}
