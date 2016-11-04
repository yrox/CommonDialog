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
        public SomeClassWithSuitableName()
        {
            messging = new Messaging(data);
        }
        private IEnumerable<IAccount> accs; 
        private UnitOfWork data;

        private Messaging messging;

        public void SendMesage(GeneralContact genContact, string message)
        {
            var accToSend = accs.Single(a => a.AccountType == genContact.Messages.Last().Type);
        }
    }
}
