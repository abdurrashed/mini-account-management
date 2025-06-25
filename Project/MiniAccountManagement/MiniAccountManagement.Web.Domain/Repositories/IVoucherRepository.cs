using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.Entities;

namespace MiniAccountManagement.Web.Domain.Repositories
{
    public interface IVoucherRepository
    {
        Task SaveVoucherAsync(Voucher voucher);

    }
}
