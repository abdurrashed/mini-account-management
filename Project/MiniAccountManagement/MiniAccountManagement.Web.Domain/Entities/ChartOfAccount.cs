using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAccountManagement.Web.Domain.Entities
{
    public class ChartOfAccount
    {
        public Guid Id { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }
        public Guid? ParentAccountId { get; set; }
        public string AccountType { get; set; } 
        public string? Description { get; set; } 

        public List<ChartOfAccount> Children { get; set; } = new();
    }
}
