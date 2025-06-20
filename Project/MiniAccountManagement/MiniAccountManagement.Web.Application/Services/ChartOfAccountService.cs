using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Infrastructure;
using MiniAccountManagement.Web.Domain.Services;

namespace MiniAccountManagement.Web.Application.Services
{
    public class ChartOfAccountService : IChartOfAccountService
    {
        public Task CreateAccountAsync(ChartOfAccount account)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAccountAsync(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChartOfAccount>> GetAllAccountsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAccountAsync(ChartOfAccount account)
        {
            throw new NotImplementedException();
        }
    }
}
