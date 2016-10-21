using System;
using System.Collections.Generic;
using System.Linq;
using BaseEntyties;
using System.Data.Entity;
using DataLayer.Interfaces;
using RefactorThis.GraphDiff;

namespace DataLayer.Repositories
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
        public IEnumerable<Message> Find(Func<Message, bool> predicate)
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
