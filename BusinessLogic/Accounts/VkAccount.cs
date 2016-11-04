using System;
using System.Collections.Generic;
using System.Linq;
using BaseEntyties;
using BusinessLogic.Interfaces;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;

namespace BusinessLogic.Accounts
{
    public class VkAccount : IAccount
    {
        public VkAccount(Account acc)
        {
            AccountType = acc.Type;
            api = new VkApi();
            Authorize(acc.Login, acc.Password);
        }

        public string AccountType { get; }

        private VkApi api;

        private Func<string> code = () =>
        {
            Console.Write("Please enter code: ");
            string value = Console.ReadLine();

            return value;
        };
        private void Authorize(string login, string password)
        {
            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 5678626,
                Login = "+375298857813",
                Password = "MWAHAHA17954gotteenn90years",
                Settings = Settings.All,
                TwoFactorAuthorization = code
            });
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return api.Friends.Get(api.UserId.Value).Select(item => GetContact(item.Id)).ToList();
        }
        public Contact GetContact(string name)
        {
            var c = api.Friends.Search(new FriendsSearchParams
            {
                UserId = (long)api.UserId,
                Query = name
            });
            return EntytiesMapper.Map(c.First());
        }
        public Contact GetContact(long id)
        {
            return EntytiesMapper.Map(api.Users.Get(id));
        }

        public void SendMessage(string text, Contact contact)
        {
            api.Messages.Send(new MessagesSendParams
            {
                UserId = api.UserId,
                Message = text
            });
        }
        public IEnumerable<Message> GetMessagesByContact(Contact contact)
        {
           var s = api.Messages.GetHistory(
                new MessagesGetHistoryParams
                {
                    UserId = GetContact(contact.Name).ContactIdentifier,
                    StartMessageId = -1
                });
            return EntytiesMapper.Map(s.Messages);
        } 

    }
}
