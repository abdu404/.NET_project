using ContactManager.Models;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        // Navigation property for contacts associated with this category
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}