using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Services;

namespace MiniAccountManagement.Web.Pages.Accounts
{
    public class EditModel : PageModel
    {
        private readonly IChartOfAccountService _service;

        public EditModel(IChartOfAccountService service)
        {
            _service = service;
        }

        [BindProperty]
        public ChartOfAccountInputModel Account { get; set; } = new();

        public List<SelectListItem> ParentAccounts { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var accounts = await _service.GetAllAsync();

            var accountToEdit = accounts.SelectMany(Flatten)
                .FirstOrDefault(a => a.Id == id);

            if (accountToEdit == null)
                return NotFound();

            Account = new ChartOfAccountInputModel
            {
                Id = accountToEdit.Id,
                AccountCode = accountToEdit.AccountCode,
                AccountName = accountToEdit.AccountName,
                AccountType = accountToEdit.AccountType,
                Description = accountToEdit.Description,
                ParentAccountId = accountToEdit.ParentAccountId
            };

            ParentAccounts = BuildParentAccountList(accounts)
                .Where(p => p.Value != id.ToString()) // Prevent parent being self
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var accounts = await _service.GetAllAsync();
                ParentAccounts = BuildParentAccountList(accounts)
                    .Where(p => p.Value != Account.Id.ToString())
                    .ToList();
                return Page();
            }

            var account = new ChartOfAccount
            {
                Id = Account.Id,
                AccountCode = Account.AccountCode,
                AccountName = Account.AccountName,
                AccountType = Account.AccountType,
                Description = Account.Description,
                ParentAccountId = Account.ParentAccountId
            };

            await _service.UpdateAccountAsync(account);

            return RedirectToPage("Index");
        }

        private List<SelectListItem> BuildParentAccountList(List<ChartOfAccount> accounts, int level = 0)
        {
            var list = new List<SelectListItem>();

            foreach (var acc in accounts)
            {
                string prefix = new string('-', level * 2);
                list.Add(new SelectListItem($"{prefix} {acc.AccountName}", acc.Id.ToString()));

                if (acc.Children?.Count > 0)
                {
                    list.AddRange(BuildParentAccountList(acc.Children, level + 1));
                }
            }

            return list;
        }

        private IEnumerable<ChartOfAccount> Flatten(ChartOfAccount acc)
        {
            yield return acc;
            if (acc.Children != null)
            {
                foreach (var child in acc.Children)
                    foreach (var c in Flatten(child))
                        yield return c;
            }
        }

        public class ChartOfAccountInputModel
        {
            public Guid Id { get; set; }

            [Required]
            public string AccountCode { get; set; } 

            [Required]
            public string AccountName { get; set; } 

            [Required]
            public string AccountType { get; set; } 

            public string? Description { get; set; }

            public Guid? ParentAccountId { get; set; }
        }
    }
}
