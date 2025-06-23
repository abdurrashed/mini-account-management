using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Infrastructure.Identity;

namespace MiniAccountManagement.Web.Infrastructure.Seed
{
    public class SeedData
    {


        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await context.Database.MigrateAsync();

           
            string[] roles = { "Admin", "Accountant", "Viewer" };
            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                }
            }

            
            var adminUser = await userManager.FindByEmailAsync("admin@qtec.com");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin123",
                    Email = "admin@qtec.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "admin@123");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Seed Accountant User
            var accountantUser = await userManager.FindByEmailAsync("accountant@qtec.com");
            if (accountantUser == null)
            {
                accountantUser = new ApplicationUser
                {
                    UserName = "accountant123",
                    Email = "accountant@qtec.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(accountantUser, "Accountant@123");
                await userManager.AddToRoleAsync(accountantUser, "Accountant");
            }

            // Seed Permissions if not present
            if (!context.ModulePermissions.Any())
            {
                var adminRole = await roleManager.FindByNameAsync("Admin");
                var accountantRole = await roleManager.FindByNameAsync("Accountant");
                var viewerRole = await roleManager.FindByNameAsync("Viewer");

 var permissions = new List<ModulePermission>
{
    // Admin Full Access
    NewPermission(adminRole.Id, "ChartOfAccounts", true, true, true, true),
    NewPermission(adminRole.Id, "VoucherEntry", true, true, false, false), // Only View & Create
    NewPermission(adminRole.Id, "UserManagement", true, true, true, true),
    NewPermission(adminRole.Id, "RoleManagement", true, true, true, true),
   

    // Accountant Limited Access
    NewPermission(accountantRole.Id, "ChartOfAccounts", true, true, true, false),
    NewPermission(accountantRole.Id, "VoucherEntry", true, true, false, false),
    NewPermission(accountantRole.Id, "UserManagement", false, false, false, false),
    NewPermission(accountantRole.Id, "RoleManagement", false, false, false, false),
    

    // Viewer Minimal Access
    NewPermission(viewerRole.Id, "ChartOfAccounts", true, false, false, false),  // Only list/view
    NewPermission(viewerRole.Id, "VoucherEntry", false, false, false, false),
    NewPermission(viewerRole.Id, "UserManagement", false, false, false, false),
    NewPermission(viewerRole.Id, "RoleManagement", false, false, false, false),

};


                context.ModulePermissions.AddRange(permissions);
                await context.SaveChangesAsync();
            }
        }

        private static ModulePermission NewPermission(string roleId, string module, bool canView, bool canCreate, bool canEdit, bool canDelete)
        {
            return new ModulePermission
            {
                Id = Guid.NewGuid(),
                RoleId = roleId,
                ModuleName = module,
                CanView = canView,
                CanCreate = canCreate,
                CanEdit = canEdit,
                CanDelete = canDelete
            };
        }




    }
}
