using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Infrastructure;
using MiniAccountManagement.Web.Domain.Repositories;
using MiniAccountManagement.Web.Infrastructure;

namespace MiniAccountManagement.Web.Infrastructure.Repositories
{
    public class ChartOfAccountRepository : IChartOfAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public ChartOfAccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task CreateAsync(ChartOfAccount account)
        {
            var parameters = new[]
            {
                new SqlParameter("@Action", "Create"),
                new SqlParameter("@Id", System.Data.SqlDbType.Int) { Value = DBNull.Value },
                new SqlParameter("@AccountName", account.AccountName),
                new SqlParameter("@AccountCode", account.AccountCode),
                new SqlParameter("@ParentAccountId", (object?)account.ParentAccountId ?? DBNull.Value),
                new SqlParameter("@AccountType", account.AccountType),
                new SqlParameter("@Description", account.Description),
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_ManageChartOfAccounts @Action, @Id, @AccountName, @AccountCode, @ParentAccountId, @AccountType, @Description", parameters);
        }

        public Task DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ChartOfAccount>> GetAllAsync()
        {



        
            var accounts = await _context.ChartOfAccounts
                .FromSqlRaw("EXEC sp_GetChartOfAccounts")
                .AsNoTracking()
                .ToListAsync();

            var lookup = new Dictionary<Guid, ChartOfAccount>();
            var rootAccounts = new List<ChartOfAccount>();

            foreach (var acc in accounts)
                lookup[acc.Id] = acc;

            foreach (var acc in accounts)
            {
                if (acc.ParentAccountId != null && lookup.ContainsKey(acc.ParentAccountId.Value))
                    lookup[acc.ParentAccountId.Value].Children.Add(acc);
                else
                    rootAccounts.Add(acc);
            }

            return rootAccounts;
        }


        

        public Task UpdateAsync(ChartOfAccount account)
        {
            throw new NotImplementedException();
        }
    }
}
