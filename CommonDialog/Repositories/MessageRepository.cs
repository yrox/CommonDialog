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
            _db = context;
            //_db.Messages.Load();
        }

        private CommonDialogContext _db;

        public bool Contains(Message item)
        {
            return _db.Messages.Any(
                     m => m.ContactIdentifier == item.ContactIdentifier &&
                     m.Text == item.Text &&
                     m.Type == item.Type &&
                     m.DateTime == item.DateTime);
        }
        public IEnumerable<Message> GetAll()
        {
            return _db.Messages;
        }
        public Message Get(int id)
        {
            return _db.Messages.Find(id);
        }
        public IEnumerable<Message> Find(Func<Message, bool> predicate)
        {
            return _db.Messages.Where(predicate).ToList();
        }

        public void Add(Message item)
        {
            if(!Contains(item))
            {
                _db.Messages.Add(item);
                //_db.Messages.Attach(item);
                //_db.Entry(item).State = EntityState.Added;
            }
            else
            {
                Update(item);
            }
        }

        public void AddRange(IEnumerable<Message> items)
        {
            foreach (var item in items.Where(item => !Contains(item)))
            {
                Add(item);
            }
        }

        public void Update(Message item)
        {
            _db.Entry(item).State = EntityState.Detached;
            _db.UpdateGraph(item, map => map.OwnedEntity(x => x.MetaContact));
        }

        public void Delete(Message item)
        {
            _db.Messages.Remove(item);
        }

        public void DeleteRange(IEnumerable<Message> messages)
        {
            foreach (var item in messages)
            {
                Delete(item);
            }
        }
    }
}
