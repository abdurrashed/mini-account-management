using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAccountManagement.Web.Domain.Services
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(string userId, string moduleName, string action);

    }
}
