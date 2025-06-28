using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagement.Web.Application.Services;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Services;
using MiniAccountManagement.Web.Infrastructure.Identity;

namespace MiniAccountManagement.Web.Pages.Voucher
{
    public class CreateModel : PageModel
    {
        private readonly IVoucherService _voucherService;
        private readonly IChartOfAccountService _accountService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPermissionService _permissionService;

        public CreateModel(IVoucherService voucherService, IChartOfAccountService accountService,IPermissionService permissionService, UserManager<ApplicationUser> userManager)
        {
            _voucherService = voucherService;
            _accountService = accountService;
            _userManager = userManager;
            _permissionService = permissionService;

        }

        [BindProperty]
        public MiniAccountManagement.Web.Domain.Entities.Voucher VoucherVM { get; set; } = new();

        public List<ChartOfAccount> Accounts { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["AccessDenied"] = "User not found or not logged in.";
                return RedirectToPage("/Index");
            }

            var userId = user.Id;
            var hasPermission = await _permissionService.HasPermissionAsync(userId, "VoucherEntry", "Create");

            if (!hasPermission)
            {
                TempData["AccessDenied"] = "You do not have permission to perform this action.";
                return RedirectToPage("/Index");
            }
            Accounts = await _accountService.GetAllAsync();

            if (VoucherVM.Entries.Count == 0)
                VoucherVM.Entries.Add(new VoucherEntry());

            if (VoucherVM.Date == default)
            {
                VoucherVM.Date = DateTime.Today;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Accounts = await _accountService.GetAllAsync();

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Validation error.";
                return Page();
            }

            try
            {
                VoucherVM.Id = Guid.NewGuid();
                foreach (var entry in VoucherVM.Entries)
                {
                    entry.Id = Guid.NewGuid();
                    entry.VoucherId = VoucherVM.Id;
                }

                await _voucherService.SaveVoucherAsync(VoucherVM);
                TempData["SuccessMessage"] = "Voucher saved successfully!";
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return Page();
            }
        }
    }

}
