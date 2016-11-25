using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BaseEntyties;

namespace BusinessLogic.Mappers
{
    public class EntytiesMapper
    {
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
            message.MetaContact.Id = metaContactId;
            return message;
        }
        public static IEnumerable<Message> Map(IEnumerable<VkNet.Model.Message> vkMesEnumerable, int metaContactId)
        {
            return vkMesEnumerable.Select(Map).ToList();
        }

        
    }
}
