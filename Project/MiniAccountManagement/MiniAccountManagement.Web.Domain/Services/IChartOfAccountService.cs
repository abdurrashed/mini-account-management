using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.Entities;

namespace MiniAccountManagement.Web.Domain.Services
{
    public interface IChartOfAccountService
    {

        Task<List<ChartOfAccount>> GetAllAccountsAsync();
        Task CreateAccountAsync(ChartOfAccount account);
        Task UpdateAccountAsync(ChartOfAccount account);
        Task DeleteAccountAsync(int accountId);
    }
}
