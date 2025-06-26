using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using MiniAccountManagement.Web.Domain.DTOS;

namespace MiniAccountManagement.Web.Pages.ManageUsers
{
    public class EditUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public EditUserModel(IUserService userService, RoleManager<ApplicationRole> roleManager)
        {
            _userService = userService;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public List<string> Roles { get; set; } = new();

        public class InputModel
        {
            public string UserId { get; set; } = "";

            [Required]
            public string UserName { get; set; } = "";

            [Required, EmailAddress]
            public string Email { get; set; } = "";

            [Required]
            public string Role { get; set; } = "";
        }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();

            Input = new InputModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role
            };

            Roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            Roles = _roleManager.Roles.Select(r => r.Name).ToList();

            if (!ModelState.IsValid)
                return Page();

            var userDto = new UserDto
            {
                UserId = Input.UserId,
                UserName = Input.UserName,
                Email = Input.Email,
                Role = Input.Role
            };

            var success = await _userService.UpdateUserAsync(userDto);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to update user.");
                return Page();
            }

            TempData["SuccessMessage"] = "User updated successfully!";
            return RedirectToPage("UserList");
        }
    }
}
