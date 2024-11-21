//using ContactManager.Models;
//using ContactManager.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;

//namespace ContactManager.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ContactDbContext _ctx;

//        public HomeController(ContactDbContext ctx)
//        {
//            _ctx = ctx;
//        }

//        public IActionResult MainPage()
//        {
//            if (User.Identity.IsAuthenticated)
//            {
//                return RedirectToAction("Index");
//            }
//            else
//            {
//                return View();
//            }
//        }

//        public IActionResult Index()
//        {
//            var contacts = _ctx.Contacts.Include(c => c.Category).OrderBy(c => c.LastName).ToList();
//            return View(contacts);
//        }

//        public IActionResult Category()
//        {
//            if (!User.Identity.IsAuthenticated)
//            {
//                return RedirectToAction("MainPage");
//            }

//            var categories = _ctx.Categories.Include(c => c.Contacts).ToList();
//            return View(categories);
//        }

//        public IActionResult Groups()
//        {
//            if (!User.Identity.IsAuthenticated)
//            {
//                return RedirectToAction("MainPage");
//            }

//            var groups = _ctx.Groups.Include(g => g.Contacts).ToList();
//            return View(groups);
//        }

//        public IActionResult CreateGroup()
//        {
//            if (!User.Identity.IsAuthenticated)
//            {
//                return RedirectToAction("MainPage");
//            }

//            var contacts = _ctx.Contacts.OrderBy(c => c.LastName).ToList();
//            return View(new GroupViewModel { Contacts = contacts });
//        }

//        [HttpPost]
//        public IActionResult CreateGroup(GroupViewModel model)
//        {
//            if (!User.Identity.IsAuthenticated)
//            {
//                return RedirectToAction("MainPage");
//            }

//            if (ModelState.IsValid)
//            {
//                var group = new ContactManager.Models.Group
//                {
//                    Name = model.Name,
//                    Contacts = _ctx.Contacts.Where(c => model.SelectedContactIds.Contains(c.ContactId)).ToList()
//                };
//                _ctx.Groups.Add(group);
//                _ctx.SaveChanges();
//                return RedirectToAction("Groups");
//            }

//            model.Contacts = _ctx.Contacts.OrderBy(c => c.LastName).ToList();
//            return View(model);
//        }

//        public IActionResult EditGroup(int id)
//        {
//            if (!User.Identity.IsAuthenticated)
//            {
//                return RedirectToAction("MainPage");
//            }

//            var group = _ctx.Groups.Include(g => g.Contacts).FirstOrDefault(g => g.GroupId == id);
//            if (group == null)
//            {
//                return NotFound();
//            }

//            var contacts = _ctx.Contacts.OrderBy(c => c.LastName).ToList();
//            var model = new GroupViewModel
//            {
//                GroupId = group.GroupId,
//                Name = group.Name,
//                Contacts = contacts,
//                SelectedContactIds = group.Contacts.Select(c => c.ContactId).ToList()
//            };

//            return View(model);
//        }

//        [HttpPost]
//        public IActionResult EditGroup(GroupViewModel model)
//        {
//            if (!User.Identity.IsAuthenticated)
//            {
//                return RedirectToAction("MainPage");
//            }

//            if (ModelState.IsValid)
//            {
//                var group = _ctx.Groups.Include(g => g.Contacts).FirstOrDefault(g => g.GroupId == model.GroupId);
//                if (group == null)
//                {
//                    return NotFound();
//                }

//                group.Name = model.Name;
//                group.Contacts = _ctx.Contacts.Where(c => model.SelectedContactIds.Contains(c.ContactId)).ToList();
//                _ctx.SaveChanges();
//                return RedirectToAction("Groups");
//            }

//            model.Contacts = _ctx.Contacts.OrderBy(c => c.LastName).ToList();
//            return View(model);
//        }

//        [HttpPost]
//        public IActionResult DeleteGroup(int id)
//        {
//            if (!User.Identity.IsAuthenticated)
//            {
//                return RedirectToAction("MainPage");
//            }

//            var group = _ctx.Groups.FirstOrDefault(g => g.GroupId == id);
//            if (group == null)
//            {
//                return NotFound();
//            }

//            _ctx.Groups.Remove(group);
//            _ctx.SaveChanges();

//            return RedirectToAction("Groups");
//        }
//    }
//}
using ContactManager.ViewModels;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContactManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContactDbContext _ctx;

        public HomeController(ContactDbContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult MainPage()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Index()
        {
            var contacts = _ctx.Contacts.Include(c => c.Category).OrderBy(c => c.LastName).ToList();
            return View(contacts);
        }

        public IActionResult Category()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MainPage");
            }

            var categories = _ctx.Categories.Include(c => c.Contacts).ToList();
            return View(categories);
        }

        public IActionResult Groups()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MainPage");
            }

            var groups = _ctx.Groups.Include(g => g.Contacts).ToList();
            return View(groups);
        }

        public IActionResult CreateGroup()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MainPage");
            }

            var contacts = _ctx.Contacts.OrderBy(c => c.LastName).ToList();
            return View(new GroupViewModel { Contacts = contacts });
        }

        [HttpPost]
        public IActionResult CreateGroup(GroupViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MainPage");
            }

            if (ModelState.IsValid)
            {
                var group = new ContactManager.Models.Group
                {
                    Name = model.Name,
                    Contacts = _ctx.Contacts.Where(c => model.SelectedContactIds.Contains(c.ContactId)).ToList()
                };
                _ctx.Groups.Add(group);
                _ctx.SaveChanges();
                return RedirectToAction("Groups");
            }

            model.Contacts = _ctx.Contacts.OrderBy(c => c.LastName).ToList();
            return View(model);
        }

        public IActionResult EditGroup(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MainPage");
            }

            var group = _ctx.Groups.Include(g => g.Contacts).FirstOrDefault(g => g.GroupId == id);
            if (group == null)
            {
                return NotFound();
            }

            var contacts = _ctx.Contacts.OrderBy(c => c.LastName).ToList();
            var model = new GroupViewModel
            {
                GroupId = group.GroupId,
                Name = group.Name,
                Contacts = contacts,
                SelectedContactIds = group.Contacts.Select(c => c.ContactId).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditGroup(GroupViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MainPage");
            }

            if (ModelState.IsValid)
            {
                var group = _ctx.Groups.Include(g => g.Contacts).FirstOrDefault(g => g.GroupId == model.GroupId);
                if (group == null)
                {
                    return NotFound();
                }

                group.Name = model.Name;
                group.Contacts = _ctx.Contacts.Where(c => model.SelectedContactIds.Contains(c.ContactId)).ToList();
                _ctx.SaveChanges();
                return RedirectToAction("Groups");
            }

            model.Contacts = _ctx.Contacts.OrderBy(c => c.LastName).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteGroup(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MainPage");
            }

            var group = _ctx.Groups.Include(g => g.Contacts).FirstOrDefault(g => g.GroupId == id);
            if (group == null)
            {
                return NotFound();
            }

            // Set the GroupId of associated contacts to null
            foreach (var contact in group.Contacts)
            {
                contact.GroupId = null;
            }

            // Save changes to update the contacts in the database
            _ctx.SaveChanges();

            // Remove the group after updating the contacts
            _ctx.Groups.Remove(group);
            _ctx.SaveChanges();

            return RedirectToAction("Groups");
        }
    }
}

