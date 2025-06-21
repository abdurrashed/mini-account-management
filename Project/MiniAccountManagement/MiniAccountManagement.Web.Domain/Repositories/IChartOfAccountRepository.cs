using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.Entities;

namespace MiniAccountManagement.Web.Domain.Repositories
{
    public interface IChartOfAccountRepository
    {

        Task<List<ChartOfAccount>> GetAllAsync();
        Task CreateAsync(ChartOfAccount account);
        Task UpdateAsync(ChartOfAccount account);
        Task DeleteAsync(Guid Id);
    
    }
}
