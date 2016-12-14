using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
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
            _account = acc;
            AccountType = acc.Type;
            _pts = Convert.ToUInt64(acc.PhoneNumber);
        }

        private Account _account;

        private VkNet.Model.LongPollServerResponse _longPoll;
        private VkNet.Model.LongPollHistoryResponse _longPollHistory;
        private ulong? _pts;
       
        private const int _appId = 5678626;
        private VkApi _api;

        public string AccountType { get; }

        private static string code;
        private Func<string> _code = () =>
        {
            return code;
        };

        //Func<string> code = () =>
        //{
        //    Console.Write("Please enter code: ");
        //    string value = Console.ReadLine();

        //    return value;
        //};

        public void Authorize(string codeValue)
        {
            code = codeValue;
            try
            {

                _api.Authorize(new ApiAuthParams
                {
                    ApplicationId = _appId,
                    Login = _account.Login,
                    Password = _account.Password,
                    Settings = Settings.All,
                    TwoFactorAuthorization = _code
                });
            }
            catch (CaptchaNeededException cEx)
            {
                ExceptionDispatchInfo.Capture(cEx).Throw();
            }
            
        }

        public void Authorize(string captcha, long sid)
        {
            _api.Authorize(new ApiAuthParams
            {
                ApplicationId = _appId,
                Login = _account.Login,
                Password = _account.Password,
                Settings = Settings.All,
                CaptchaKey = captcha,
                CaptchaSid = sid
            });
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
            try
            {
                _api.Messages.Send(new MessagesSendParams
                {
                    UserId = _api.UserId,
                    Message = message.Text
                });
            }
            catch (CaptchaNeededException cEx)
            {
                ExceptionDispatchInfo.Capture(cEx).Throw();
            }
        }
        public void SendMessage(Message message, string captcha, long sid)
        {
            _api.Messages.Send(new MessagesSendParams
            {
                UserId = _api.UserId,
                Message = message.Text,
                CaptchaKey = captcha,
                CaptchaSid = sid
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

        public IEnumerable<Message> GetNewMessages()
        {
            _longPoll = _api.Messages.GetLongPollServer(useSsl:false, needPts:true);
    
            _longPollHistory = _api.Messages.GetLongPollHistory(new MessagesGetLongPollHistoryParams
            {
                Ts = _longPoll.Ts,
                Onlines = false,
                Pts = _pts
            });
            _pts = _longPollHistory.NewPts;
            _account.PhoneNumber = _pts.ToString();
            return EntytiesMapper.Map(_longPollHistory.Messages, 0).ToList();
        }

    }
}
