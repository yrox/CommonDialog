using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using BaseEntyties;
using BusinessLogic.Accounts;
using BusinessLogic.Interfaces;
using BusinessLogic.Logic;
using Ninject;
using Ninject.Parameters;
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
            _cachedContacts = new List<Contact>();
            _accsData = GetDbAccounts();
            CreateAccs();
        }

        private ICollection<IAccount> _accs;
        private IEnumerable<Account> _accsData;
        private Messaging _messaging;
        private Contacts _contacts;
        private DataHandler _dataHandler;
        private ICollection<Contact> _cachedContacts;

        private void CreateAccs()
        {
            _accs = new List<IAccount>();
            var kernel = new StandardKernel();
            foreach (var acc in _accsData)
            {
                //_accs.Add(kernel.Get<IAccount>(acc.Type, new ConstructorArgument("acc", acc)));
                if (acc.Type == "Vk")
                    _accs.Add(new VkAccount(acc));
                if (acc.Type == "Telegram")
                    _accs.Add(new TelegramAccount(acc));
            }
        }

        private void CasheContacts(string type)
        {
            var contacts = _accs.Single(a => a.AccountType == type).GetAllContacts(); 
            foreach (var item in contacts)
            {
                _cachedContacts.Add(item);
            }
        }
        public void Authorize(string code)
        {
            try
            {
                foreach (var acc in _accs)
                {
                    acc.Authorize(code);
                }
            }

            catch (CaptchaNeededException cEx)
            {
                ExceptionDispatchInfo.Capture(cEx).Throw();
            }
            CasheContacts("Vk");
            CasheContacts("Telegram");
        }
        public void Authorize(string captcha, long sid)
        {
            foreach (var acc in _accs)
            {
                acc.Authorize(captcha, sid);
            }
            CasheContacts("Vk");
            CasheContacts("Telegram");
        }


        public void SavemetaContact(MetaContact metaContact)
        {
            _dataHandler.Save(metaContact);
        }
        public void SaveAccount(Account acc)
        {
            _dataHandler.Save(acc);
        }
        public void SaveAccounts(IEnumerable<Account> accs)
        {
            _dataHandler.SaveRange(accs);
        }
        public void DeleteMetaContact(MetaContact metaContact)
        {
            _dataHandler.Delete(metaContact);
        }
        public void DeleteMetaContacts(IEnumerable<MetaContact> metaContacts)
        {
            _dataHandler.DeleteRange(metaContacts);
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
            message.DateTime = DateTime.Now;
            _dataHandler.Save(message);
            var accToSend = _accs.Single(a => a.AccountType == message.Type);
            _messaging.SendMessage(message, accToSend);
        }
        public void SendMesage(Message message, string captcha, long sid)
        {
            message.DateTime = DateTime.Now;
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

        public IEnumerable<Message> GetNewMessages()
        {
            //var metaConIds = GetDbMetaContacts().Select(item => item.Id).ToList();
            var cont = _dataHandler.GetDbContacts().ToList();
            var allMessages = new List<Message>();
            foreach (var acc in _accs)
            {
                allMessages.AddRange(acc.GetNewMessages());
            }
            SaveAccounts(_accsData);
            var result = new List<Message>();
            foreach (var mes in allMessages)
            {
                if (cont.Any(x => x.ContactIdentifier == mes.ContactIdentifier && x.Type == mes.Type))
                {
                    mes.MetaContact =
                        cont.Find(x => x.ContactIdentifier == mes.ContactIdentifier && x.Type == mes.Type).MetaContact;
                    result.Add(mes);

                }
            }
            _dataHandler.SaveRange(result);
            return result;
            //return allMessages.Where(m => cont.Any(x => x.ContactIdentifier == m.ContactIdentifier && x.Type == m.Type));
        }

        public IEnumerable<Contact> LoadContactsOfType(string type)
        {
            return _cachedContacts.Where(x => x.Type == type);
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
