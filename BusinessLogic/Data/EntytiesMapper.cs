using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BaseEntyties;

namespace BusinessLogic.Data
{
    public class EntytiesMapper
    {
        public static Contact Map(VkNet.Model.User vkCon)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<VkNet.Model.User, Contact>()
                .ForMember("ContactIdentifier", x => x.MapFrom(c => c.Id))
                .ForMember("Name", x => x.MapFrom(c => c.FirstName + " " + c.LastName)));
            return Mapper.Map<VkNet.Model.User, Contact>(vkCon);
        }
        public static IEnumerable<Contact> Map(IEnumerable<VkNet.Model.User> vkConEnumerable)
        {
            return vkConEnumerable.Select(Map).ToList();
        }

        public static Message Map(VkNet.Model.Message vkMes)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<VkNet.Model.Message, Message>()
                .ForMember("Text", x => x.MapFrom(c => c.Body))
                .ForMember("DateTime", x => x.MapFrom(c => c.Date))
                .ForMember("ContactIdentifier", x => x.MapFrom(c => c.UserId)));
            return Mapper.Map<VkNet.Model.Message, Message>(vkMes);
        }
        public static IEnumerable<Message> Map(IEnumerable<VkNet.Model.Message> vkMesEnumerable)
        {
            return vkMesEnumerable.Select(Map).ToList();
        }

        
    }
}
