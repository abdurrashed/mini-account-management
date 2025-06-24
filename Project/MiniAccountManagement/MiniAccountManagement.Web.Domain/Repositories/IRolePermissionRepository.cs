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
        Task<List<ModulePermission>> GetAllAsync();
        Task<ModulePermission> GetByIdAsync(Guid id);
        Task AssignAsync(ModulePermission permission);
        Task UpdateAsync(ModulePermission permission);
        Task DeleteAsync(Guid id);

    }
}
