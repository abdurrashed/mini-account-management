using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure.Identity;
using NuGet.Protocol.Core.Types;

namespace MiniAccountManagement.Web.Pages.ManageRoles
{
    public class ListPermissionsModel(IRolePermissionService RolepermissionService,IPermissionService permissionService, UserManager<ApplicationUser> usermanager, RoleManager<ApplicationRole> roleManager) : PageModel
    {
        private readonly IRolePermissionService _RolepermissionService = RolepermissionService;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
        private readonly UserManager<ApplicationUser> _userManager = usermanager;
        private readonly IPermissionService _permissionService = permissionService;

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
            CanCreate = await _permissionService.HasPermissionAsync(userId, "RoleManagement", "Create");
            CanEdit = await _permissionService.HasPermissionAsync(userId, "RoleManagement", "Edit");
            CanDelete = await _permissionService.HasPermissionAsync(userId, "RoleManagement", "Delete");


            var perms = await _RolepermissionService.GetAllAsync();
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
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _RolepermissionService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Permission deleted successfully!";
            return RedirectToPage();
        }

       
    }
}
