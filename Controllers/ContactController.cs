using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactDbContext _ctx;

        public ContactController(ContactDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _ctx.Categories.ToList();
            ViewBag.Action = "Add";
            return View("AddEdit", new Contact());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var contact = _ctx.Contacts.Find(id);
            ViewBag.Categories = _ctx.Categories.ToList();
            ViewBag.Action = "Edit";
            return View("AddEdit", contact);
        }

        [HttpPost]
        public IActionResult AddEdit(Contact contact, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    var fileName = Path.GetFileName(imageFile.FileName);
                    var fullPath = Path.Combine(imagePath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    contact.ImagePath = $"/images/{fileName}";
                }

                if (contact.ContactId == 0)
                {
                    contact.DateAdded = DateTime.Now;
                    _ctx.Contacts.Add(contact);
                }
                else
                {
                    _ctx.Contacts.Update(contact);
                }
                _ctx.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Categories = _ctx.Categories.ToList();
            return View(contact);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contact = _ctx.Contacts.Find(id);
            return View(contact);
        }

        [HttpPost]
        public IActionResult Delete(Contact contact)
        {
            _ctx.Contacts.Remove(contact);
            _ctx.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var contact = _ctx.Contacts.Include(contact => contact.Category).FirstOrDefault(c => c.ContactId == id);
            return View(contact);
        }

        [HttpGet]
        public IActionResult Search(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var contact = _ctx.Contacts.Include(c => c.Category)
                                           .FirstOrDefault(c => c.FirstName.Contains(searchString) ||
                                                                c.Email.Contains(searchString) ||
                                                                c.PhoneNumber.Contains(searchString));

                if (contact != null)
                {
                    return RedirectToAction("Details", new { id = contact.ContactId });
                }
                else
                {
                    ViewBag.Message = "No contacts found matching the search criteria.";
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

