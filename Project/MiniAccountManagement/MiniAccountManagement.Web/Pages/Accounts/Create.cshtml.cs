using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Services;

namespace MiniAccountManagement.Web.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly IChartOfAccountService _service;

        public CreateModel(IChartOfAccountService service)
        {
            _service = service;
        }

        [BindProperty]
        public ChartOfAccount Account { get; set; } = new();

        public List<SelectListItem> ParentAccounts { get; set; } = new();

        public async Task OnGetAsync()
        {
            var accounts = await _service.GetAllAccountsAsync();
            ParentAccounts = FlattenAccounts(accounts);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            await _service.CreateAccountAsync(Account);

            return RedirectToPage("Index");
        }

        private List<SelectListItem> FlattenAccounts(List<ChartOfAccount> accounts, int level = 0)
        {
            var list = new List<SelectListItem>();
            string prefix = new string('-', level * 2);

            foreach (var acc in accounts)
            {
                list.Add(new SelectListItem { Value = acc.Id.ToString(), Text = prefix + acc.AccountName });
                if (acc.Children != null)
                {
                    list.AddRange(FlattenAccounts(acc.Children, level + 1));
                }
            }
            return list;
        }
    }
}
