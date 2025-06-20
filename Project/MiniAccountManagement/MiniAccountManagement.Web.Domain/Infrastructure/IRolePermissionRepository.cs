using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.Entities;

namespace MiniAccountManagement.Web.Domain.Infrastructure
{
    public interface IRolePermissionRepository
    {
        Task<List<ModulePermission>> GetByRoleAsync(string roleId);
        Task AssignAsync(ModulePermission permission);
        Task RemoveAsync(int permissionId);


    }
}
