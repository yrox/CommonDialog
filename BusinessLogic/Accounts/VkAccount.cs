using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Drawing;
using System.Net.Mime;
using BaseEntyties;
using BusinessLogic.Interfaces;
using BusinessLogic.Mappers;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Exception;
using VkNet.Model.RequestParams;

namespace BusinessLogic.Accounts
{
    public class VkAccount : IAccount
    {
        public VkAccount(Account acc)
        {
            _api = new VkApi();
            Authorize(acc.Login, acc.Password);
            AccountType = acc.Type;
        }
        
        public string AccountType { get; }

        private const int _appId = 5678626;
        private VkApi _api;
        private string _captchaKey;
        private long _captchsSid;

        private Func<string> _code = () =>
        {
            Console.Write("Please enter _code: ");
            string value = Console.ReadLine();

            return value;
        };
        public void Authorize(string login, string password)
        {
            try
            {
                _api.Authorize(new ApiAuthParams
                {
                    ApplicationId = _appId,
                    Login = "",
                    Password = "",
                    Settings = Settings.All,
                    TwoFactorAuthorization = _code
                });
            }
            catch (CaptchaNeededException cEx)
            {
                CaptchaHandler(cEx);
            }
            _api.Authorize(new ApiAuthParams
            {
                ApplicationId = _appId,
                Login = "",
                Password = "",
                Settings = Settings.All,
                CaptchaKey = _captchaKey,
                CaptchaSid = _captchsSid
            });
        }

        private void CaptchaHandler(CaptchaNeededException ex)
        {
            var client = new WebClient();
            var image =new Bitmap(new MemoryStream(client.DownloadData(ex.Img)));
            //string captchaUrl = ex.Img;
        }
        

        public IEnumerable<Contact> GetAllContacts()
        {
            return _api.Friends.Get(_api.UserId.Value).Select(item => GetContact(item.Id)).ToList();
        }
        public Contact GetContact(string name)
        {
            var c = _api.Friends.Search(new FriendsSearchParams
            {
                UserId = (long)_api.UserId,
                Query = name
            });
            return EntytiesMapper.Map(c.First());
        }
        public Contact GetContact(long id)
        {
            return EntytiesMapper.Map(_api.Users.Get(id));
        }

        public void SendMessage(Message message)
        {
            _api.Messages.Send(new MessagesSendParams
            {
                UserId = _api.UserId,
                Message = message.Text
            });
        }
        public IEnumerable<Message> GetMessagesByContact(Contact contact)
        {
           var s = _api.Messages.GetHistory(
                new MessagesGetHistoryParams
                {
                    UserId = GetContact(contact.Name).ContactIdentifier,
                    StartMessageId = -1
                });
            return EntytiesMapper.Map(s.Messages, contact.MetaContact.Id);
        } 

    }
}
