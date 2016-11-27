using System.Collections.Generic;
using BaseEntyties;

namespace BusinessLogic.Interfaces
{
    public interface IAccount : IMessaging, IContacts
    {
        void Authorize(string code);
        void Authorize(string captcha, long sid);
        string AccountType { get; }
        
    }
}
