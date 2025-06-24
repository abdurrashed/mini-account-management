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

       

        public async Task AssignAsync(ModulePermission permission)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_AssignPermissionToRole @RoleId, @ModuleName, @CanView, @CanCreate, @CanEdit, @CanDelete",
                new SqlParameter("@RoleId", permission.RoleId),
                new SqlParameter("@ModuleName", permission.ModuleName),
                new SqlParameter("@CanView", permission.CanView),
                new SqlParameter("@CanCreate", permission.CanCreate),
                new SqlParameter("@CanEdit", permission.CanEdit),
                new SqlParameter("@CanDelete", permission.CanDelete));
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Database.ExecuteSqlRawAsync(
              "EXEC sp_DeleteModulePermission @Id",
              new SqlParameter("@Id", id)
          );
        }

     


        public async Task<List<ModulePermission>> GetAllAsync()
        {
            return await _context.ModulePermissions
                .FromSqlRaw("EXEC sp_GetAllModulePermissions")
                .ToListAsync();
        }

        public async Task<ModulePermission> GetByIdAsync(Guid id)
        {
            var param = new SqlParameter("@Id", id);
            var list = await _context.ModulePermissions
                .FromSqlRaw("EXEC sp_GetModulePermissionById @Id", param)
                .ToListAsync();
            return list.Count > 0 ? list[0] : null;
        }

     

        public async Task UpdateAsync(ModulePermission permission)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_UpdateModulePermission @Id, @RoleId, @ModuleName, @CanView, @CanCreate, @CanEdit, @CanDelete",
                new SqlParameter("@Id", permission.Id),
                new SqlParameter("@RoleId", permission.RoleId),
                new SqlParameter("@ModuleName", permission.ModuleName),
                new SqlParameter("@CanView", permission.CanView),
                new SqlParameter("@CanCreate", permission.CanCreate),
                new SqlParameter("@CanEdit", permission.CanEdit),
                new SqlParameter("@CanDelete", permission.CanDelete)
            );
        }
    }
}
