using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoring
{
    public class Dialog
    {
        public int Id { get; set; }
        public int DialogIdentifier { get; set; }
        public string Type { get; set; }

        public virtual User User { get; set; }
        public virtual Account Account { get; set; }
        public virtual IEnumerable<Message> Messages { get; set; }
        public virtual IEnumerable<Contact> Contacts { get; set; }
    }
}
