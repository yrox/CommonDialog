using System;
using  DataLayer.Repositories;

namespace DataLayer
{
    public class UnitOfWork : IDisposable
    {
        private CommonDialogContext _db = new CommonDialogContext();
        private AccountRepository _accountRepository;
        private MetaContactRepository _metaContactRepository;
        private MessageRepository _messageRepository;
        private ContactRepository _contactRepository;

        public AccountRepository Accounts
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_db);
                }
                return _accountRepository;
            }
        }
        public MetaContactRepository MetaContacts
        {
            get
            {
                if (_metaContactRepository == null)
                {
                    _metaContactRepository = new MetaContactRepository(_db);
                }
                return _metaContactRepository;
            }
        }
        public MessageRepository Messages
        {
            get
            {
                if (_messageRepository == null)
                {
                    _messageRepository = new MessageRepository(_db);
                }
                return _messageRepository;
            }
        }
        public ContactRepository Contacts
        {
            get
            {
                if (_contactRepository == null)
                {
                    _contactRepository = new ContactRepository(_db);
                }
                return _contactRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this._disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
