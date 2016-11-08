using System.Collections.Generic;
using System.Linq;
using BaseEntyties;
using BusinessLogic.Interfaces;

namespace BusinessLogic.Logic
{
    public class Messaging
    {
        public void SendMessage(Message message, IMessaging acc)
        {
            acc.SendMessage(message);
        }
        public IEnumerable<Message> LoadHistory(Contact contact, IMessaging acc)
        {
            return acc.GetMessagesByContact(contact);
        }
    }
}
