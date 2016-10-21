namespace BaseEntyties
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int ContactIdentifier { get; set; }
        public string Type { get; set; }

        public virtual GeneralContact GeneralContact { get; set; }
        
    }
}
