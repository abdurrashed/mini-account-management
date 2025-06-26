using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagement.Web.Application.Services;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Services;

namespace MiniAccountManagement.Web.Pages.Voucher
{
    public class CreateModel : PageModel
    {
        private readonly IVoucherService _voucherService;
        private readonly IChartOfAccountService _accountService;

        public CreateModel(IVoucherService voucherService, IChartOfAccountService accountService)
        {
            _voucherService = voucherService;
            _accountService = accountService;
        }

        [BindProperty]
        public MiniAccountManagement.Web.Domain.Entities.Voucher VoucherVM { get; set; } = new();

        public List<ChartOfAccount> Accounts { get; set; } = new();

        public async Task OnGetAsync()
        {
            Accounts = await _accountService.GetAllAsync();

            if (VoucherVM.Entries.Count == 0)
                VoucherVM.Entries.Add(new VoucherEntry());

            if (VoucherVM.Date == default)
            {
                VoucherVM.Date = DateTime.Today;
            }
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
