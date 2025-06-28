using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure.Identity;
using MiniAccountManagement.Web.Domain.DTOS;
using MiniAccountManagement.Web.Application.Services;

namespace MiniAccountManagement.Web.Pages.ManageUsers
{
    public class UserListModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPermissionService _permissionService;

        public UserListModel(IUserService userService, UserManager<ApplicationUser> userManager,IPermissionService permissionService)
        {
            _userService = userService;
            _userManager = userManager;
            _permissionService = permissionService;
        }

        public List<UserWithRolesDto> Users { get; set; } = new();
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["AccessDenied"] = "User not found or not logged in.";
                return RedirectToPage("/Index");
            }

            var userId = user.Id;
            CanCreate = await _permissionService.HasPermissionAsync(userId, "UserManagement", "Create");
            CanEdit = await _permissionService.HasPermissionAsync(userId, "UserManagement", "Edit");
            CanDelete = await _permissionService.HasPermissionAsync(userId, "UserManagement", "Delete");


            Users = await _userService.GetAllUsersAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string userId)
        {
            var success = await _userService.DeleteUserAsync(userId);

            TempData["SuccessMessage"] = "User deleted successfully.";

            return RedirectToPage();
        }
    }
}
