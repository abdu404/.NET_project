using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ContactManager.Models;
using ContactManager.ViewModels;

namespace ContactManager.Controllers
{
    public class AppUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ContactDbContext _context;
        private readonly ILogger<AppUserController> _logger;

        public AppUserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ContactDbContext context, ILogger<AppUserController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new SettingVM
            {
                EmailAddress = user.Email,
                Name = user.Name
            };

            return View(model);
        }

        public async Task<IActionResult> Settings()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View("Error");
            }

            var settingsVM = new SettingVM
            {
                EmailAddress = user.Email,
                Name = user.Name
            };

            return View(settingsVM);
        }

        [HttpPost]
        public async Task<IActionResult> Settings(SettingVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit");
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                user.Name = model.Name;
                user.Email = model.EmailAddress;

                if (!string.IsNullOrEmpty(model.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    _logger.LogInformation($"Password reset token generated: {token}");

                    var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Failed to update password");
                        return View(model);
                    }
                }

                if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.ProfilePicture.CopyToAsync(memoryStream);
                        user.ProfilePictureData = memoryStream.ToArray();
                    }
                }

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to update user information");
                    return View(model);
                }

                await _signInManager.SignOutAsync();
                _logger.LogInformation("User signed out successfully");

                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation("User signed back in successfully");

                HttpContext.Session.SetString("UserName", user.UserName);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "User not found");
            return View(model);
        }
    }
}
