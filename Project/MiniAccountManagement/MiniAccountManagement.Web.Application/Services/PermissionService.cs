using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure;
using MiniAccountManagement.Web.Infrastructure.Identity;

namespace MiniAccountManagement.Web.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public PermissionService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<bool> HasPermissionAsync(string userId, string moduleName, string action)
        {
           
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

         
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || roles.Count == 0)
                return false;

            var roleName = roles[0];

         
            IdentityRole role = null;
            foreach (var r in _roleManager.Roles)
            {
                if (r.Name == roleName)
                {
                    role = r;
                    break;
                }
            }

            if (role == null)
                return false;

            var roleId = role.Id;

       
            var permissionList = _context.ModulePermissions.ToList();
            ModulePermission matched = null;

            foreach (var p in permissionList)
            {
                if (p.RoleId == roleId && p.ModuleName == moduleName)
                {
                    matched = p;
                    break;
                }
            }

            if (matched == null)
                return false;

          
            switch (action)
            {
                case "View": return matched.CanView;
                case "Create": return matched.CanCreate;
                case "Edit": return matched.CanEdit;
                case "Delete": return matched.CanDelete;
                default: return false;
            }
        }

    }
}
