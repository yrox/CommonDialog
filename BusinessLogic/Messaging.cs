using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseEntyties;
using BusinessLogic.Interfaces;
using DataLayer;

namespace BusinessLogic
{
    public class Messaging
    {
        public Messaging(UnitOfWork data)
        {
            this.data = data;
        }

        private UnitOfWork data;

        public IEnumerable<Message> GetMessagesOfType(GeneralContact genContact, string type)
        {
            return genContact.Messages.Where(mes => mes.Type == type);
        }
        public IEnumerable<Message> GetAllMessages(GeneralContact genContact)
        {
            return genContact.Messages;
        }

        public void SendMessage(Contact cont, IMessaging acc, string message)
        {
            acc.SendMessage(message, cont);
        }
        public IEnumerable<Message> LoadHistory(Contact contact, IMessaging acc)
        {
            return acc.GetMessagesByContact(contact);
        }
    }
}
