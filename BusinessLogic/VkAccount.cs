using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using BaseEntyties;
using BusinessLogic.Interfaces;
using VkNet;
using VkNet.Model.RequestParams;

namespace BusinessLogic
{
    public class VkAccount : IAccount
    {
        public VkAccount(string login, string password)
        {
            api = new VkApi();
            Authorize(login, password);
            InitializeMapper();
        }

        private void InitializeMapper()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<VkNet.Model.User, Contact>()
            .ForMember("ContactIdentifier", x => x.MapFrom(c => c.Id))
            .ForMember("Name", x => x.MapFrom(c => c.FirstName + " " + c.LastName)));
        }

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
                ApplicationId = 0,
                Login = login,
                Password = password,
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
                Query = name
            });
            return Mapper.Map<Contact>(c);
        }
        public Contact GetContact(long id)
        {
            return Mapper.Map<Contact>(api.Users.Get(id));
        }

        public void SendMessage(string text, Contact contact)
        {
            api.Messages.Send(new MessagesSendParams
            {
                UserId = api.UserId,
                Message = text
            });
        }


    }
}
