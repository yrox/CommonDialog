using System;

namespace BaseEntyties
{
    public class Message 
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
        public int ContactIdentifier { get; set; }

        public virtual MetaContact MetaContact { get; set; }

    }
}
