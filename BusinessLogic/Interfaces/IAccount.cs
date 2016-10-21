using System.Collections.Generic;
using BaseEntyties;

namespace BusinessLogic.Interfaces
{
    public interface IAccount
    {
        IEnumerable<Contact> GetAllContacts();
        Contact GetContact(long id);
        Contact GetContact(string nameOrPhoneNumber);

        void SendMessage(string text, Contact contact);
 
    }
}
