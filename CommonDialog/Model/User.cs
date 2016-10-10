using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoring
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password  { get; set; }

        public virtual IEnumerable<Account> Accounts { get; set; }
        public virtual IEnumerable<Dialog> AllDialogs { get; set; }
        public virtual IEnumerable<Contact> AllContacts { get; set; }
    }
}
