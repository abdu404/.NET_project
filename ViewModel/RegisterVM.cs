
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        //[FileExtensions(Extensions = "jpg,jpeg,png,gif", ErrorMessage = "Please upload an image file (jpg, jpeg, png, gif).")]
        public IFormFile? ProfileImage { get; set; }
    }
}
