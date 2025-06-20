using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Infrastructure;

namespace MiniAccountManagement.Web.Application.Services
{
    public class RolePermissionService : IRolePermissionRepository
    {



        public Task AssignAsync(ModulePermission permission)
        {
            throw new NotImplementedException();
        }

        public Task<List<ModulePermission>> GetByRoleAsync(string roleId)
        {
            throw new NotImplementedException();
        }
         
        public Task RemoveAsync(int permissionId)
        {
            throw new NotImplementedException();
        }
    }
}
