using System.Collections.Generic;
using BaseEntyties;

namespace BusinessLogic.Interfaces
{
    public interface IAccount : IMessaging, IContacts
    {
        string AccountType { get; }
    }
}
