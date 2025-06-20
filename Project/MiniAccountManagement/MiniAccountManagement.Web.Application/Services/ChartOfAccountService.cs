using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure;



namespace MiniAccountManagement.Web.Application.Services
{
    public class ChartOfAccountService : IChartOfAccountService
    {

        private readonly ApplicationDbContext _context;

        public ChartOfAccountService(ApplicationDbContext context)
        {
            _context = context;
        }

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
