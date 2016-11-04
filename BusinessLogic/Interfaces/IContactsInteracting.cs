using System.Collections.Generic;
using BaseEntyties;

namespace BusinessLogic.Interfaces
{
    public interface IContactsInteracting
    {
        IEnumerable<Contact> GetAllContacts();
        Contact GetContact(long id);
        Contact GetContact(string nameOrPhoneNumber);
    }
}
