using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure.Identity;

namespace MiniAccountManagement.Web.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly IChartOfAccountService _service;
        private readonly IPermissionService _permissionService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(
            IChartOfAccountService service,
            IPermissionService permissionService,
            UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _permissionService = permissionService;
            _userManager = userManager;
        }

        [BindProperty]
        public ChartOfAccount Account { get; set; } = new();

        public List<SelectListItem> ParentAccounts { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["AccessDenied"] = "User not found or not logged in.";
                return RedirectToPage("/Index");
            }

            var userId = user.Id;
            var hasPermission = await _permissionService.HasPermissionAsync(userId, "ChartOfAccounts", "Create");

            if (!hasPermission)
            {
                TempData["AccessDenied"] = "You do not have permission to perform this action.";
                return RedirectToPage("/Index");
            }
            var accounts = await _service.GetAllAsync();
            ParentAccounts = FlattenAccounts(accounts);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["AccessDenied"] = "User not found or not logged in.";
                return RedirectToPage("/Index");
            }

            var userId = user.Id;
            var hasPermission = await _permissionService.HasPermissionAsync(userId, "ChartOfAccounts", "Create");

            if (!hasPermission)
            {
                TempData["AccessDenied"] = "You do not have permission to perform this action.";
                return RedirectToPage("/Index");
            }


            if (!ModelState.IsValid) return Page();

            await _service.CreateAccountAsync(Account);

            TempData["SuccessMessage"] = "Account created successfully!";

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
