using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Model;
using RefactorThis.GraphDiff;
using System.Data.Entity;

namespace DataLayer.DataAccess
{
    public class GeneralContactRepository : IRepository<GeneralContact>
    {
        public GeneralContactRepository(CommonDialogContext context)
        {
            db = context;
        }

        private CommonDialogContext db;

        public IEnumerable<GeneralContact> GetAll()
        {
            return db.GeneralContacts;
        }
        public GeneralContact Get(int id)
        {
            return db.GeneralContacts.Find(id);
        }
        public IEnumerable<GeneralContact> Find(Func<GeneralContact, bool> predicate)
        {
            return db.GeneralContacts.Where(predicate).ToList();
        }

        public void Add(GeneralContact item)
        {
            db.GeneralContacts.Add(item);
            //db.GeneralContacts.Attach(item);
            //db.Entry(item).State = EntityState.Added;
            // Update(item);
        }

        public void Update(GeneralContact item)
        {
            db.Entry(item).State = EntityState.Detached;
            db.UpdateGraph(item, map => map.OwnedCollection(x => x.Contacts));
            db.UpdateGraph(item, map => map.OwnedCollection(x => x.Messages));
        }

        public void Delete(GeneralContact item)
        {
            db.Entry(item).State = EntityState.Deleted;
        }
    }
}
