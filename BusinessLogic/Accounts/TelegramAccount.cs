using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using BaseEntyties;
using BusinessLogic.Interfaces;
using BusinessLogic.Mappers;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core;
using VkNet.Exception;
using VkNet.Model.RequestParams;

namespace BusinessLogic.Accounts
{
    public class TelegramAccount : IAccount
    {
        public TelegramAccount(Account acc)
        {
            _account = acc;
            AccountType = acc.Type;
            _api = new TelegramClient(_appId, _appHash);
            _connected = _api.ConnectAsync().Result;
        }

        private Account _account;
        private bool _connected = false;

        private string _authHash;

        private const int _appId = 52208;
        private const string _appHash = "1bbb7b3160f43db0b541e4a27be3cb55";
        private TelegramClient _api;

        private TeleSharp.TL.TLUser GetTlContact(int id)
        {
            var absContacts = _api.GetContactsAsync().Result.users.lists.ToList();
            var contacts = absContacts.Select(item => item as TLUser).ToList();
            return contacts.Find(x => x.id == id);
        }

        public string AccountType { get; }

        public void Authorize(string codeValue)
        {
            if (!_api.IsUserAuthorized())
            {
                _authHash = _api.SendCodeRequestAsync(_account.PhoneNumber).Result;
                //throw exception

            }
        }
        public void Authorize(string captcha, long sid)
        {
            _api.MakeAuthAsync("+375298857813", _authHash, captcha);
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return _api.GetContactsAsync().Result.users.lists.Select(EntytiesMapper.Map).ToList();
        }
        public Contact GetContact(string name)
        {
            return GetAllContacts().ToList().Find(x => x.Name == name);
        }
        public Contact GetContact(long id)
        {
            return GetAllContacts().ToList().Find(x => x.ContactIdentifier == id);
        }

        public void SendMessage(Message message)
        {
            var cont = GetTlContact(message.ContactIdentifier);
            var accessHash = cont.access_hash.Value;
            var id = message.ContactIdentifier;
            var peer = new TLInputPeerUser {access_hash = accessHash, user_id = id};
            _api.SendMessageAsync(
                peer, message.Text);
        }
        public void SendMessage(Message message, string captcha, long sid)
        {
            _api.SendMessageAsync(
                new TLInputPeerUser
                {
                    access_hash = GetTlContact(message.ContactIdentifier).access_hash.Value,
                    user_id = message.ContactIdentifier
                }, message.Text);
        }
        public IEnumerable<Message> GetMessagesByContact(Contact contact)
        {
            var dialogs = (TLDialogs)_api.GetUserDialogsAsync().Result;
            var messages = EntytiesMapper.Map(dialogs.messages.lists, 0).ToList();
            return messages.Where(x => x.ContactIdentifier == contact.ContactIdentifier);

        }

        public IEnumerable<Message> GetNewMessages()
        {
            var dialogs = (TLDialogs)_api.GetUserDialogsAsync().Result;
            return EntytiesMapper.Map(dialogs.messages.lists, 0).ToList();
        }

    }
}

