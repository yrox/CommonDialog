using System.Collections.Generic;
using BaseEntyties;

namespace BusinessLogic.Interfaces
{
    public interface IContacts
    {
        IEnumerable<Contact> GetAllContacts();
        Contact GetContact(long id);
        Contact GetContact(string nameOrPhoneNumber);
    }
}
