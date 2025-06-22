using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAccountManagement.Web.Domain.Entities
{
    public class ModulePermission
    {
        public Guid Id { get; set; }
        public string RoleId { get; set; }
        public string ModuleName { get; set; } 
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }


    }
}
