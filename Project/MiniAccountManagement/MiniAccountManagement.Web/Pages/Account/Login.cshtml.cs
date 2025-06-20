using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagement.Web.Infrastructure.Identity;
using System.ComponentModel.DataAnnotations;

namespace MiniAccountManagement.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public string? ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Username is required")]
            public string UserName { get; set; } = "";

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = "";

           
        }

        public void OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");

            if (!ModelState.IsValid)
                return Page();

            var result = await _signInManager.PasswordSignInAsync(
           Input.UserName,
           Input.Password,
           isPersistent: false,
           lockoutOnFailure: false);


            if (result.Succeeded)
            {
                return LocalRedirect(ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return Page();
        }
    }
}
