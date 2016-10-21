using System;
using  DataLayer.Repositories;

namespace DataLayer
{
    public class UnitOfWork : IDisposable
    {
        private CommonDialogContext db = new CommonDialogContext();
        private AccountRepository accountRepository;
        private GeneralContactRepository generalContactRepository;
        private MessageRepository messageRepository;
        private ContactRepository contactRepository;

        public AccountRepository Accounts
        {
            get
            {
                if (accountRepository == null)
                {
                    accountRepository = new AccountRepository(db);
                }
                return accountRepository;
            }
        }
        public GeneralContactRepository GeneralContacts
        {
            get
            {
                if (generalContactRepository == null)
                {
                    generalContactRepository = new GeneralContactRepository(db);
                }
                return generalContactRepository;
            }
        }
        public MessageRepository Messages
        {
            get
            {
                if (messageRepository == null)
                {
                    messageRepository = new MessageRepository(db);
                }
                return messageRepository;
            }
        }
        public ContactRepository Contacts
        {
            get
            {
                if (contactRepository == null)
                {
                    contactRepository = new ContactRepository(db);
                }
                return contactRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
