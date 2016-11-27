using System.Collections.Generic;
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

        public void SendMessage(Message message, IMessaging acc, string captcha, long sid)
        {
            acc.SendMessage(message, captcha, sid);
        }
        public IEnumerable<Message> LoadHistory(Contact contact, IMessaging acc)
        {
            return acc.GetMessagesByContact(contact);
        }
    }
}
