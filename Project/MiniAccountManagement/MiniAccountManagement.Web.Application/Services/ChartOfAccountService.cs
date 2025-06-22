using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Repositories;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure;
using MiniAccountManagement.Web.Infrastructure.Repositories;

namespace MiniAccountManagement.Web.Application.Services
{
    public class ChartOfAccountService : IChartOfAccountService
    {

        private readonly IChartOfAccountRepository _repository;

        public ChartOfAccountService(IChartOfAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAccountAsync(ChartOfAccount account)
        {
            await _repository.CreateAsync(account);
        }

        public Task DeleteAccountAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ChartOfAccount>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task UpdateAccountAsync(ChartOfAccount account)
        {
            await _repository.UpdateAsync(account);
        }
    }
}
