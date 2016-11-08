using System;
using System.Collections.Generic;
using System.Linq;
using BaseEntyties;
using RefactorThis.GraphDiff;
using System.Data.Entity;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class GeneralContactRepository : IRepository<GeneralContact>
    {
        public GeneralContactRepository(CommonDialogContext context)
        {
            _db = context;
        }

        private CommonDialogContext _db;

        public bool Contains(GeneralContact genContact)
        {
            return _db.GeneralContacts.Any(g => g.Id == genContact.Id || g.Id == 0);
        }
        public IEnumerable<GeneralContact> GetAll()
        {
            return _db.GeneralContacts;
        }
        public GeneralContact Get(int id)
        {
            return _db.GeneralContacts.Find(id);
        }
        public IEnumerable<GeneralContact> Find(Func<GeneralContact, bool> predicate)
        {
            return _db.GeneralContacts.Where(predicate).ToList();
        }

        public void Add(GeneralContact item)
        {
            _db.GeneralContacts.Add(item);
            
        }

        public void Update(GeneralContact item)
        {
            _db.Entry(item).State = EntityState.Detached;
            _db.UpdateGraph(item, map => map.OwnedCollection(x => x.Contacts));
            _db.UpdateGraph(item, map => map.OwnedCollection(x => x.Messages));
        }

        public void Delete(GeneralContact item)
        {
            _db.Entry(item).State = EntityState.Deleted;
        }
    }
}
