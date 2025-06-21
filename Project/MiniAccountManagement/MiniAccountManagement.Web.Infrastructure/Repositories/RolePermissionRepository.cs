using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Infrastructure;

namespace MiniAccountManagement.Web.Infrastructure.Infrastructure
{
    public class RolePermissionRepository : IRolePermissionRepository
    {

        private readonly ApplicationDbContext _context;

        public RolePermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
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
