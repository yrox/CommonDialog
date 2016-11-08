using System.Collections.Generic;
using BaseEntyties;
using BusinessLogic.Interfaces;

namespace BusinessLogic.Logic
{
    public class Contacts
    {
        public IEnumerable<Contact> GetAllContacts(IContacts acc)
        {
            return acc.GetAllContacts();
        }
        public Contact GetContact(IContacts acc, long id)
        {
            return acc.GetContact(id);
        }
        public Contact GetContact(IContacts acc, string nameOrPhoneNumber)
        {
            return acc.GetContact(nameOrPhoneNumber);
        }

    }
}
