using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure.Identity;

namespace MiniAccountManagement.Web.Pages.ManageRoles
{
    public class EditPermissionModel : PageModel
    {
        private readonly IRolePermissionService _permissionService;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public EditPermissionModel(IRolePermissionService permissionService, RoleManager<ApplicationRole> roleManager)
        {
            _permissionService = permissionService;
            _roleManager = roleManager;
        }

        [BindProperty]
        public ModulePermission Permission { get; set; }

        public List<IdentityRole> Roles { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Permission = await _permissionService.GetByIdAsync(id);
            if (Permission == null)
                return NotFound();

            Roles = new List<IdentityRole>(_roleManager.Roles);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Roles = new List<IdentityRole>(_roleManager.Roles);
                return Page();
            }

            await _permissionService.UpdateAsync(Permission);
            return RedirectToPage("ListPermissions");
        }
    }
}
