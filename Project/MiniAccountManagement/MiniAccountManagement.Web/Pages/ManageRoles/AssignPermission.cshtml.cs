using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAccountManagement.Web.Application.Services;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure.Identity;

namespace MiniAccountManagement.Web.Pages.ManageRoles
{
    public class AssignPermissionModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IRolePermissionService _RolepermissionService;
        private readonly IPermissionService _permissionService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AssignPermissionModel(RoleManager<ApplicationRole> roleManager, IPermissionService permissionService , UserManager<ApplicationUser> userManager, IRolePermissionService  RolepermissionService)
        {
            _roleManager = roleManager;
            _RolepermissionService = RolepermissionService;
            _permissionService = permissionService;
            _userManager = userManager;
        }

        [BindProperty]
        public ModulePermission Permission { get; set; }

        public SelectList RoleList { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["AccessDenied"] = "User not found or not logged in.";
                return RedirectToPage("/Index");
            }

            var userId = user.Id;
            var hasPermission = await _permissionService.HasPermissionAsync(userId, "RoleManagement", "Create");

            if (!hasPermission)
            {
                TempData["AccessDenied"] = "You do not have permission to perform this action.";
                return RedirectToPage("/Index");
            }

            LoadRoles();
            Permission = new ModulePermission();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            LoadRoles();

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Validation error.";
                return Page();
            }

           
            await _RolepermissionService.AssignPermissionAsync(Permission);
            TempData["SuccessMessage"] = "Assigned permission successfully!";
            return RedirectToPage("ListPermissions");
        }

        private void LoadRoles()
        {
            var roles = _roleManager.Roles.ToList();
            RoleList = new SelectList(roles, "Id", "Name");
        }
    }
}