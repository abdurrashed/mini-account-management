using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAccountManagement.Web.Domain.Entities
{
    public class Voucher
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string ReferenceNo { get; set; }
        public string Type { get; set; }
        public List<VoucherEntry> Entries { get; set; } = new();

    }
}
