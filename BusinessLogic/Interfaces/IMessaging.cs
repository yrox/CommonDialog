using System.Collections.Generic;
using BaseEntyties;

namespace BusinessLogic.Interfaces
{
    public interface IMessaging
    {
        void SendMessage(string text, Contact contact);
        IEnumerable<Message> GetMessagesByContact(Contact contact);
    }
}
