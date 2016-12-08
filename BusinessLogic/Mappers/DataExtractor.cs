using System.Collections.Generic;
using BaseEntyties;

namespace BusinessLogic.Mappers
{
    public class DataExtractor
    {
        public IEnumerable<Contact> Extract(IEnumerable<Contact> data)
        {
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
                    MetaContact = new MetaContact
                    {
                        Name = item.MetaContact.Name,
                        Id = item.MetaContact.Id
                    }
                });
            }
            return result;
        }

        public IEnumerable<Message> Extract(IEnumerable<Message> data)
        {
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
                    MetaContact = new MetaContact
                    {
                        Id = item.MetaContact.Id,
                        Name = item.MetaContact.Name
                    }
                });
            }
            return result;
        }

    }
}
