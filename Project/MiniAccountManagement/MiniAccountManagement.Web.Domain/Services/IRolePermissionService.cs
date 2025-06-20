using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.Entities;

namespace MiniAccountManagement.Web.Domain.Services
{
    public interface IRolePermissionService
    {
        Task<List<ModulePermission>> GetPermissionsByRoleAsync(string roleId);
        Task AssignPermissionAsync(ModulePermission permission);
        Task RemovePermissionAsync(int permissionId);
    }
}
