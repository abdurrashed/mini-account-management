using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Repositories;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure.Repositories;

namespace MiniAccountManagement.Web.Application.Services
{
    public class VoucherService : IVoucherService
    {

        private readonly IVoucherRepository _voucherRepository;

        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }
        public async Task SaveVoucherAsync(Voucher voucher)
        {
            await _voucherRepository.SaveVoucherAsync(voucher);
        }
    }
}
