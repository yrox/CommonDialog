﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public VkAccount()//(Account acc)
        {
            _api = new VkApi();
            Authorize();
            //Authorize(acc.Login, acc.Password);
            //AccountType = acc.Type;
        }
        
        public string AccountType { get; }


        private VkApi _api;

        private Func<string> _code = () =>
        {
            Console.Write("Please enter _code: ");
            string value = Console.ReadLine();

            return value;
        };
        private void Authorize()//(string login, string password)
        {
            try
            {
                _api.Authorize(new ApiAuthParams
                {
                    ApplicationId = 5678626,
                    Login = "",
                    Password = "",
                    Settings = Settings.All,
                    TwoFactorAuthorization = _code
                });
            }
            catch (CaptchaNeededException cEx)
            {
                
            }
        }

        private void CaptchaHandler(CaptchaNeededException ex)
        {
            //string captchaUrl = ex.Img;
            //string captchaKey = SomeCaptchaHandler(captchaUrl); /// Обработка капчи: загружаем через что-то изображение, выводим его, вводим ответ, идем дальше
            //_api.Message.Send(..... , ex.Sid, captchaKey);
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
            return EntytiesMapper.Map(s.Messages, contact.GeneralContact.Id);
        } 

    }
}
