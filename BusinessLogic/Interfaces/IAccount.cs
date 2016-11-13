using System.Collections.Generic;
using BaseEntyties;

namespace BusinessLogic.Interfaces
{
    public interface IAccount : IMessaging, IContacts
    {
        //IAccount CreateAccount(Account acc);
        string AccountType { get; }
        
    }
}
