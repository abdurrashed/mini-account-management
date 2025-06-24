using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ModulePermission>> GetAllAsync()
        {
            return await _context.ModulePermissions
                .FromSqlRaw("EXEC sp_GetAllModulePermissions")
                .ToListAsync();
        }

        public Task<ModulePermission> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int permissionId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ModulePermission permission)
        {
            throw new NotImplementedException();
        }
    }
}
