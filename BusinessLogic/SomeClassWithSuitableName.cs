using System;
using System.Collections.Generic;
using System.Linq;
using BaseEntyties;
using BusinessLogic.Interfaces;
using DataLayer;

namespace BusinessLogic
{
    public class SomeClassWithSuitableName
    {
        public SomeClassWithSuitableName(IEnumerable<IAccount> accs)
        {
            this.accs = accs;
            data = new UnitOfWork();
            messaging = new Messaging(data);
            contactsOperating = new ContactsOperating(data);
        }
        
        private UnitOfWork data;

        private IEnumerable<IAccount> accs;
        private Messaging messaging;
        private ContactsOperating contactsOperating;

        public void SendMesage(GeneralContact genContact, string message)
        {
            var lastMessageType = genContact.Messages.Last().Type;
            SendMssageTo(genContact, message, lastMessageType);
        }
        public void SendMssageTo(GeneralContact genContact, string message, string type)
        {
            var accToSend = accs.Single(a => a.AccountType == type);
            var conToSend = genContact.Contacts.Single(c => c.Type == type);
            messaging.SendMessage(conToSend, accToSend, message);
        }
        public IEnumerable<Message> GetMessageHistory(GeneralContact genContact)
        {
            return genContact.Messages;
        }
        public IEnumerable<Message> GetMessageHistoryOfType(GeneralContact genContact, string type)
        {
            return genContact.Messages.Where(m => m.Type == type);
        }
        public IEnumerable<Message> LoadMessageHistoryOfContact(Contact contact)
        {
            return accs.Single(a => a.AccountType == contact.Type).GetMessagesByContact(contact);
        }

        public IEnumerable<Contact> GetAllContacts(GeneralContact genContact)
        {
            return genContact.Contacts;
        }
        public IEnumerable<Contact> GetAllContactsOfType(string type)
        {
            return accs.Single(a => a.AccountType == type).GetAllContacts();
        } 
        public Contact GetContact(string type, int id)
        {
            return contactsOperating.GetContact(accs.Single(c => c.AccountType == type), id);
        }
        public Contact GetContact(string type, string nameOrPhoneNumber)
        {
            return contactsOperating.GetContact(accs.Single(c => c.AccountType == type), nameOrPhoneNumber);
        }
    }
}
