using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagement.Web.Infrastructure.Identity;
using System.ComponentModel.DataAnnotations;

namespace MiniAccountManagement.Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required]
            public string UserName { get; set; } = "";

            [Required]
            [EmailAddress]
            public string Email { get; set; } = "";

            [Required]
            [DataType(DataType.Password)]
            [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
            public string Password { get; set; } = "";

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password")]
            public string ConfirmPassword { get; set; } = "";

            [Required]
            public string Role { get; set; } = "Viewer";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = new ApplicationUser
            {
                UserName = Input.UserName.Trim(),
                Email = Input.Email.Trim()
            };

            bool hasError = false;

            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Input.Email", "This email is already registered.");
                hasError = true;
            }

            var existingUsername = await _userManager.FindByNameAsync(user.UserName);
            if (existingUsername != null)
            {
                ModelState.AddModelError("Input.UserName", "This username is already taken.");
                hasError = true;
            }

            if (hasError)
                return Page();

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                var roleName = Input.Role.Trim();

                var matchedRole = await _roleManager.FindByNameAsync(roleName);
                if (matchedRole == null)
                {
                    ModelState.AddModelError(string.Empty, $"Role '{roleName}' does not exist.");
                    return Page();
                }

                var createdUser = await _userManager.FindByNameAsync(user.UserName);
                if (createdUser == null)
                {
                    ModelState.AddModelError(string.Empty, "User creation failed. Please try again.");
                    return Page();
                }

                var roleResult = await _userManager.AddToRoleAsync(createdUser, matchedRole.Name);
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                    return Page();
                }

                // OPTIONAL: Check if role assigned correctly
                var userRoles = await _userManager.GetRolesAsync(createdUser);
                Console.WriteLine($"User '{createdUser.UserName}' assigned roles: {string.Join(", ", userRoles)}");

                await _signInManager.SignInAsync(createdUser, isPersistent: false);
                return RedirectToPage("/Index");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return Page();
        }  }
    }
 