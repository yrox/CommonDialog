using System.Collections.Generic;
using BaseEntyties;

namespace BusinessLogic.Mappers
{
    public class DataExtractor
    {
        public IEnumerable<Contact> Extract(IEnumerable<Contact> data)
        {
            if (data == null)
                return new List<Contact>();
            var result = new List<Contact>();
            foreach (var item in data)
            {
                result.Add(new Contact
                {
                    Name = item.Name,
                    ContactIdentifier = item.ContactIdentifier,
                    Id = item.Id,
                    Type = item.Type,
                    PhoneNumber = item.PhoneNumber,
                    MetaContactId = item.MetaContactId
                });
            }
            return result;
        }

        public IEnumerable<Message> Extract(IEnumerable<Message> data)
        {
            if (data == null)
                return new List<Message>();
            var result = new List<Message>();
            foreach (var item in data)
            {
                result.Add(new Message
                {
                    Id = item.Id,
                    ContactIdentifier = item.ContactIdentifier,
                    Text = item.Text,
                    Type = item.Type,
                    DateTime = item.DateTime,
                    MetaContactId = item.MetaContactId
                });
            }
            return result;
        }

    }
}
