using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure.Identity;

namespace MiniAccountManagement.Web.Pages.ManageRoles
{
    public class AssignPermissionModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IRolePermissionService _permissionService;

        public AssignPermissionModel(RoleManager<ApplicationRole> roleManager, IRolePermissionService permissionService)
        {
            _roleManager = roleManager;
            _permissionService = permissionService;
        }

        [BindProperty]
        public ModulePermission Permission { get; set; }

        public SelectList RoleList { get; set; }

        public void OnGet()
        {
            LoadRoles();
            Permission = new ModulePermission();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            LoadRoles();

            if (!ModelState.IsValid)
                return Page();

            await _permissionService.AssignPermissionAsync(Permission);
            return RedirectToPage("ListPermissions");
        }

        private void LoadRoles()
        {
            var roles = _roleManager.Roles.ToList();
            RoleList = new SelectList(roles, "Id", "Name");
        }
    }
}