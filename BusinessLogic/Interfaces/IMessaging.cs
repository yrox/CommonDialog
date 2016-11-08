using System.Collections.Generic;
using BaseEntyties;

namespace BusinessLogic.Interfaces
{
    public interface IMessaging
    {
        void SendMessage(Message message);
        IEnumerable<Message> GetMessagesByContact(Contact contact);
    }
}
