using System.Collections.Generic;

namespace BaseEntyties
{
    public class MetaContact 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        
    }
}
