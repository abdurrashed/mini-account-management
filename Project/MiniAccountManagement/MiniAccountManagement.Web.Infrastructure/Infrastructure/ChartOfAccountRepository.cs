using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Infrastructure;

namespace MiniAccountManagement.Web.Infrastructure.Infrastructure
{
    public class ChartOfAccountRepository : IChartOfAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public ChartOfAccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Task CreateAsync(ChartOfAccount account)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChartOfAccount>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ChartOfAccount account)
        {
            throw new NotImplementedException();
        }
    }
}
