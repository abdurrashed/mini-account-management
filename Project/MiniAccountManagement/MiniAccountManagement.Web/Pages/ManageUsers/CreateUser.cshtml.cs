using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagement.Web.Domain.DTOS;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure.Identity;

namespace MiniAccountManagement.Web.Pages.ManageUsers
{
    public class CreateUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateUserModel(IUserService userService, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _roleManager = roleManager;
            _userManager = userManager;
           
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public List<string> Roles { get; set; } = new();

        public class InputModel
        {
            [Required]
            public string UserName { get; set; } = "";

            [Required]
            [EmailAddress]
            public string Email { get; set; } = "";

            [Required]
            [MinLength(6)]
            public string Password { get; set; } = "";

            [Required]
            public string Role { get; set; } = "";
        }

        public void OnGet()
        {
            Roles = _roleManager.Roles.Select(r => r.Name).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Roles = _roleManager.Roles.Select(r => r.Name).ToList();

            if (!ModelState.IsValid)
                return Page();

            var userDto = new UserDto
            {
                UserName = Input.UserName.Trim(),
                Email = Input.Email.Trim(),
                Password = Input.Password,
                Role = Input.Role.Trim()
            };

            bool hasError = false;

            var existingUser = await _userManager.FindByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Input.Email", "This email is already registered.");
                hasError = true;
            }

            var existingUsername = await _userManager.FindByNameAsync(userDto.UserName);
            if (existingUsername != null)
            {
                ModelState.AddModelError("Input.UserName", "This username is already taken.");
                hasError = true;
            }

            if (hasError)
                return Page();

            var result = await _userService.CreateUserAsync(userDto);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Failed to create user.");
                return Page();
            }

            TempData["SuccessMessage"] = "User created successfully!";
            return RedirectToPage("UserList");
        }
    }
}
