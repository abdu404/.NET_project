using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.ViewModels
{
    public class GroupViewModel
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public List<int> SelectedContactIds { get; set; } = new List<int>();
    }
}

