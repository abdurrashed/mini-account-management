using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure.Identity;

namespace MiniAccountManagement.Web.Pages.ManageRoles
{
    public class ListPermissionsModel(IRolePermissionService permissionService, RoleManager<ApplicationRole> roleManager) : PageModel
    {
        private readonly IRolePermissionService _permissionService = permissionService;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

        public class PermissionViewModel
        {
            public Guid Id { get; set; }
            public string RoleId { get; set; }
            public string RoleName { get; set; }
            public string ModuleName { get; set; }
            public bool CanView { get; set; }
            public bool CanCreate { get; set; }
            public bool CanEdit { get; set; }
            public bool CanDelete { get; set; }
        }

        public List<PermissionViewModel> Permissions { get; set; }

        public async Task OnGetAsync()
        {
            var perms = await _permissionService.GetAllAsync();
            var roles = _roleManager.Roles.ToList();

            Permissions = perms.Select(p =>
            {
                var roleName = roles.FirstOrDefault(r => r.Id == p.RoleId)?.Name ?? "Unknown";
                return new PermissionViewModel
                {
                    Id = p.Id,
                    RoleId = p.RoleId,
                    RoleName = roleName,
                    ModuleName = p.ModuleName,
                    CanView = p.CanView,
                    CanCreate = p.CanCreate,
                    CanEdit = p.CanEdit,
                    CanDelete = p.CanDelete
                };
            }).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _permissionService.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}
