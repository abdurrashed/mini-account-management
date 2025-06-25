using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.Entities;

namespace MiniAccountManagement.Web.Domain.Services
{
    public interface IVoucherService
    {
        Task SaveVoucherAsync(Voucher voucher);


    }
}
