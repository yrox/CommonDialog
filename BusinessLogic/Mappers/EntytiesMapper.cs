using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BaseEntyties;

namespace BusinessLogic.Mappers
{
    public class EntytiesMapper
    {
        private static DateTime ConvertFromUnixTimestamp(int timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(timestamp);
        }
        public static Message Map(TeleSharp.TL.TLAbsMessage mes, int metaContactId)
        {
            var tlMes = mes as TeleSharp.TL.TLMessage;
            Mapper.Initialize(cfg => cfg.CreateMap<TeleSharp.TL.TLMessage, Message>()
                .ForMember("Text", x => x.MapFrom(c => c.message))
                .ForMember("ContactIdentifier", x => x.MapFrom(c => c.from_id)));
            var message = Mapper.Map<TeleSharp.TL.TLMessage, Message>(tlMes);
            message.Type = "Telegram";
            message.DateTime = ConvertFromUnixTimestamp(tlMes.date);
            message.MetaContact = new MetaContact();
            message.MetaContact.Id = metaContactId;
            return message;
        }
        public static IEnumerable<Message> Map(IEnumerable<TeleSharp.TL.TLAbsMessage> tlMesEnumerable, int metaContactId)
        {
            return tlMesEnumerable.Select(Map).ToList();
        }


        public static Contact Map(VkNet.Model.User vkCon)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<VkNet.Model.User, Contact>()
                .ForMember("ContactIdentifier", x => x.MapFrom(c => c.Id))
                .ForMember("Name", x => x.MapFrom(c => c.FirstName + " " + c.LastName)));
            var contact = Mapper.Map<VkNet.Model.User, Contact>(vkCon);
            contact.Type = "Vk";
            return contact;
        }
        public static IEnumerable<Contact> Map(IEnumerable<VkNet.Model.User> vkConEnumerable)
        {
            return vkConEnumerable.Select(Map).ToList();
        }

        public static Message Map(VkNet.Model.Message vkMes, int metaContactId)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<VkNet.Model.Message, Message>()
                .ForMember("Text", x => x.MapFrom(c => c.Body))
                .ForMember("DateTime", x => x.MapFrom(c => c.Date))
                .ForMember("ContactIdentifier", x => x.MapFrom(c => c.UserId)));
            var message = Mapper.Map<VkNet.Model.Message, Message>(vkMes);
            message.Type = "Vk";
            message.MetaContact = new MetaContact();
            message.MetaContact.Id = metaContactId;
            return message;
        }
        public static IEnumerable<Message> Map(IEnumerable<VkNet.Model.Message> vkMesEnumerable, int metaContactId)
        {
            return vkMesEnumerable.Select(Map).ToList();
        }

        public static Contact Map(TeleSharp.TL.TLAbsUser con)
        {
            var tlCon = con as TeleSharp.TL.TLUser;
            if (tlCon.last_name != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<TeleSharp.TL.TLUser, Contact>()
                .ForMember("ContactIdentifier", x => x.MapFrom(c => c.id))
                .ForMember("Name", x => x.MapFrom(c => c.first_name + " " + c.last_name))
                .ForMember("PhoneNumber", x => x.MapFrom(c => c.phone)));
            }
            else
            {
                Mapper.Initialize(cfg => cfg.CreateMap<TeleSharp.TL.TLUser, Contact>()
                .ForMember("ContactIdentifier", x => x.MapFrom(c => c.id))
                .ForMember("Name", x => x.MapFrom(c => c.first_name))
                .ForMember("PhoneNumber", x => x.MapFrom(c => c.phone)));
            }
            var contact = Mapper.Map<TeleSharp.TL.TLAbsUser, Contact>(tlCon);
            contact.Id = 0;
            contact.Type = "Telegram";
            return contact;
        }
        public static IEnumerable<Contact> Map(IEnumerable<TeleSharp.TL.TLAbsUser> tlConEnumerable)
        {
            return tlConEnumerable.Select(Map).ToList();
        }


        
    }
}
