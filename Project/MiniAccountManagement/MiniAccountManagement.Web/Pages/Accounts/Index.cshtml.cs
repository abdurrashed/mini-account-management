using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Services;

namespace MiniAccountManagement.Web.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly IChartOfAccountService _service;

        public IndexModel(IChartOfAccountService service)
        {
            _service = service;
        }

        public List<ChartOfAccount> Accounts { get; set; } = new();

        public async Task OnGetAsync()
        {
            Accounts = await _service.GetAllAsync();
        }
    }
}
