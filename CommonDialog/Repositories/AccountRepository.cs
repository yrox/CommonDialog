using System;
using System.Collections.Generic;
using System.Linq;
using BaseEntyties;
using System.Data.Entity;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        public AccountRepository(CommonDialogContext context)
        {
            _db = context;
        }

        private CommonDialogContext _db;

        public bool Contains(Account item)
        {
            return _db.Accounts.Any(a => a.AccountIdentifier == item.AccountIdentifier && a.Type == item.Type);
        }
        public IEnumerable<Account> GetAll()
        {
            return _db.Accounts;
        }
        public Account Get(int id)
        {
            return _db.Accounts.Find(id);
        }
        public IEnumerable<Account> Find(Func<Account, Boolean> predicate)
        {
            return _db.Accounts.Where(predicate).ToList();
        }

        public void Add(Account item)
        {
            _db.Accounts.Attach(item);
            _db.Entry(item).State = EntityState.Added;
        }

        public void Update(Account item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Account item)
        {
            _db.Entry(item).State = EntityState.Deleted;
        }

    }
}
