using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoring
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int ContactIdentifier { get; set; }
        public string Type { get; set; }
        
        public virtual User User { get; set; }
        public virtual Account Account { get; set; }
        public virtual IEnumerable<Dialog> Dialogs { get; set; }
    }
}
