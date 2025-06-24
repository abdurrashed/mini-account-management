using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Infrastructure;
using MiniAccountManagement.Web.Domain.Repositories;
using MiniAccountManagement.Web.Domain.Services;

namespace MiniAccountManagement.Web.Application.Services
{
    public class RolePermissionService : IRolePermissionService
    {

        private readonly IRolePermissionRepository _repository;

        public RolePermissionService(IRolePermissionRepository repository)
        {
            _repository = repository;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ModulePermission>> GetAllAsync() => _repository.GetAllAsync();

        public Task<ModulePermission> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task UpdateAsync(ModulePermission permission) => _repository.UpdateAsync(permission);
    }
}
