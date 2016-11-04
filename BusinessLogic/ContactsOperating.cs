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
    public class ContactsOperating
    {
        public ContactsOperating(UnitOfWork data)
        {
            this.data = data;
        }

        private UnitOfWork data;

        public IEnumerable<Contact> GetAllContacts(IContactsOperating acc)
        {
            return acc.GetAllContacts();
        }

        public Contact GetContact(IContactsOperating acc, long id)
        {
            return acc.GetContact(id);
        }

        public Contact GetContact(IContactsOperating acc, string nameOrPhoneNumber)
        {
            return acc.GetContact(nameOrPhoneNumber);
        }

    }
}
