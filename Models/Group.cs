namespace ContactManager.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
