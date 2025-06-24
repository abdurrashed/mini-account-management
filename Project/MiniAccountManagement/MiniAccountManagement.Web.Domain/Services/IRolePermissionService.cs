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
        Task AssignPermissionAsync(ModulePermission permission);
        Task<List<ModulePermission>> GetAllAsync();
        Task<ModulePermission> GetByIdAsync(Guid id);
        Task UpdateAsync(ModulePermission permission);
        Task DeleteAsync(Guid id);
    }
}
