using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using BaseEntyties;
using BusinessLogic.Accounts;
using BusinessLogic.Interfaces;
using BusinessLogic.Logic;
using Ninject;
using VkNet.Exception;

namespace BusinessLogic
{
    public class API
    {
        public API()
        {
            _dataHandler = DataHandler.CreateDataHandler();
            _contacts = new Contacts();
            _messaging = new Messaging();
            CreateAccs();
            
        }

        private IList<IAccount> _accs;
        private Messaging _messaging;
        private Contacts _contacts;
        private DataHandler _dataHandler;

        private void CreateAccs()
        {
            _accs = new List<IAccount>();
            var kernel = new StandardKernel();
            foreach (var acc in _dataHandler.GetDbAccounts())
            {
                //_accs.Add(kernel.Get<IAccount>(acc.Type, new ConstructorArgument("acc", acc)));
                _accs.Add(new VkAccount(acc));
            }
        }
        public void Authorize(string code)
        {
            try
            {
                foreach (var acc in _accs)
                {
                    if (acc.AccountType == "Vk")
                        acc.Authorize(code);
                }
            }

            catch (CaptchaNeededException cEx)
            {
                ExceptionDispatchInfo.Capture(cEx).Throw();
            }
        }
        public void Authorize(string captcha, long sid)
        {
            foreach (var acc in _accs)
            {
                acc.Authorize(captcha, sid);
            }
        }


        public void SavemetaContact(MetaContact metaContact)
        {
            _dataHandler.Save(metaContact);
        }
        public void SaveAccount(Account acc)
        {
            _dataHandler.Save(acc);
        }
        public IEnumerable<Message> GetDbMessageHistory(MetaContact metaContact)
        {
            return _dataHandler.GetDbMessageHistory(metaContact);
        }
        public IEnumerable<MetaContact> GetDbMetaContacts()
        {
            return _dataHandler.GetDbmetaContacts();
        }
        public IEnumerable<Contact> GetDbContactsOf(MetaContact metaContact)
        {
            return _dataHandler.GetDbContactsOf(metaContact);
        }
        public IEnumerable<Account> GetDbAccounts()
        {
            return _dataHandler.GetDbAccounts();
        }

        public void SendMesage(Message message)
        {
            _dataHandler.Save(message);
            var accToSend = _accs.Single(a => a.AccountType == message.Type);
            _messaging.SendMessage(message, accToSend);
        }//initalize datetime
        public void SendMesage(Message message, string captcha, long sid)
        {
            _dataHandler.Save(message);
            var accToSend = _accs.Single(a => a.AccountType == message.Type);
            _messaging.SendMessage(message, accToSend, captcha, sid);
        }

        public IEnumerable<Message> LoadMessageHistoryOfContact(Contact contact)
        {
            var messages = _accs.Single(a => a.AccountType == contact.Type).GetMessagesByContact(contact).ToList();
            _dataHandler.SaveRange(messages);
            return messages;
        }
        public IEnumerable<Message> LoadMessageHistoryOfMetaContact(MetaContact metaContact)
        {
            var messages = new List<Message>();
            foreach (var c in metaContact.Contacts)
            {
                messages.AddRange(LoadMessageHistoryOfContact(c));
            }
            _dataHandler.SaveRange(messages);
            return messages.OrderBy(m => m.DateTime); 
        }

        public IEnumerable<Contact> LoadContactsOfType(string type)
        {
            return _accs.Single(a => a.AccountType == type).GetAllContacts();
        }
        public IEnumerable<Contact> LoadAllContacts()
        {
            var cont = new List<Contact>();
            foreach (var acc in _accs)
            {
                cont.AddRange(LoadContactsOfType(acc.AccountType));
            }
            return cont;
        }
        public Contact GetContact(string type, int id)
        {
            return _contacts.GetContact(_accs.Single(c => c.AccountType == type), id);
        }
        public Contact GetContact(string type, string nameOrPhoneNumber)
        {
            return _contacts.GetContact(_accs.Single(c => c.AccountType == type), nameOrPhoneNumber);
        }
    }
}
