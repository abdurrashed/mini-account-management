using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.Services;

namespace MiniAccountManagement.Web.Application.Services
{
    public class PermissionService : IPermissionService
    {
        public Task<bool> HasPermissionAsync(string userId, string moduleName, string action)
        {
            throw new NotImplementedException();
        }
    }
}
