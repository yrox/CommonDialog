using System.Collections.Generic;
using BaseEntyties;

namespace BusinessLogic.Interfaces
{
    public interface IMessaging
    {
        void SendMessage(Message message);
        void SendMessage(Message message, string captcha, long sid);
        IEnumerable<Message> GetMessagesByContact(Contact contact);
        IEnumerable<Message> GetNewMessages();
    }
}
