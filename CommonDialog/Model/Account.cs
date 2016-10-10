using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoring
{
    public class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int AccountIdentifier { get; set; }
        public string Type { get; set; }

        public virtual User User { get; set; }
        public virtual IEnumerable<Dialog> Dialogs { get; set; }
        public virtual IEnumerable<Contact> Contacts { get; set; }
    }
}
