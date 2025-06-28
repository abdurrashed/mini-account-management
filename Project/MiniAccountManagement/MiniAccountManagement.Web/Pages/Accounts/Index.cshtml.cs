using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagement.Web.Application.Services;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure.Identity;

namespace MiniAccountManagement.Web.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly IChartOfAccountService _service;
        private readonly IPermissionService _permissionService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(IChartOfAccountService service, UserManager<ApplicationUser> userManager, IPermissionService permissionService)
        {
            _service = service;
            _permissionService = permissionService;
            _userManager = userManager;
        }

        public List<ChartOfAccount> Accounts { get; set; } = new();
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

        public async Task OnGetAsync()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userId = user.Id;
                CanCreate = await _permissionService.HasPermissionAsync(userId, "ChartOfAccounts", "Create");
                CanEdit = await _permissionService.HasPermissionAsync(userId, "ChartOfAccounts", "Edit");
                CanDelete = await _permissionService.HasPermissionAsync(userId, "ChartOfAccounts", "Delete");
            }
            Accounts = await _service.GetAllAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            

            await _service.DeleteAccountAsync(id);
            TempData["SuccessMessage"] = "Account deleted successfully!";

            return RedirectToPage(); 
        }


    }
}
