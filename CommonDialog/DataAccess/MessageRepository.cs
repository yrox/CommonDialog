using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.DataAccess;
using DataLayer.Model;
using System.Data.Entity;
using RefactorThis.GraphDiff;

namespace CommonDialog.DataAccess
{
    public class MessageRepository : IRepository<Message>
    {
        public MessageRepository(CommonDialogContext context)
        {
            db = context;
        }

        private CommonDialogContext db;
        public IEnumerable<Message> GetAll()
        {
            return db.Messages;
        }
        public Message Get(int id)
        {
            return db.Messages.Find(id);
        }
        public IEnumerable<Message> Find(Func<Message, Boolean> predicate)
        {
            return db.Messages.Where(predicate).ToList();
        }

        public void Add(Message item)
        {
            db.Messages.Attach(item);
            db.Entry(item).State = EntityState.Added;
        }

        public void Update(Message item)
        {
            db.Entry(item).State = EntityState.Detached;
            db.UpdateGraph(item, map => map.OwnedEntity(x => x.GeneralContact));
        }

        public void Delete(Message item)
        {
            db.Entry(item).State = EntityState.Deleted;
        }
    }
}
