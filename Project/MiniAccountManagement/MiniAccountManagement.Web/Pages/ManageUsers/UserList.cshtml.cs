using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure.Identity;
using MiniAccountManagement.Web.Domain.DTOS;

namespace MiniAccountManagement.Web.Pages.ManageUsers
{
    public class UserListModel : PageModel
    {
        private readonly IUserService _userService;

        public UserListModel(IUserService userService)
        {
            _userService = userService;
        }

        public List<UserWithRolesDto> Users { get; set; } = new();

       

        public async Task OnGetAsync()
        {
            Users = await _userService.GetAllUsersAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string userId)
        {
            var success = await _userService.DeleteUserAsync(userId);

            TempData["SuccessMessage"] = "User deleted successfully.";

            return RedirectToPage();
        }
    }
}
