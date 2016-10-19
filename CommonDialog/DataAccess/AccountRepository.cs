using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Model;
using System.Data.Entity;

namespace DataLayer.DataAccess
{
    public class AccountRepository : IRepository<Account>
    {
        public AccountRepository(CommonDialogContext context)
        {
            db = context;
        }

        private CommonDialogContext db;
        public IEnumerable<Account> GetAll()
        {
            return db.Accounts;
        }
        public Account Get(int id)
        {
            return db.Accounts.Find(id);
        }
        public IEnumerable<Account> Find(Func<Account, Boolean> predicate)
        {
            return db.Accounts.Where(predicate).ToList();
        }

        public void Add(Account item)
        {
            db.Accounts.Attach(item);
            db.Entry(item).State = EntityState.Added;
        }

        public void Update(Account item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Account item)
        {
            db.Entry(item).State = EntityState.Deleted;
        }

    }
}
