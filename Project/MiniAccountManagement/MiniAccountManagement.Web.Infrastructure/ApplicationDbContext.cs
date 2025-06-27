using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Infrastructure.Identity;

namespace MiniAccountManagement.Web.Infrastructure
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {

       public DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
       public DbSet<ModulePermission> ModulePermissions { get; set; }

        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherEntry> VoucherEntries { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ChartOfAccount>().HasKey(a => a.Id);
            builder.Entity<ModulePermission>().HasKey(mp => mp.Id);

            builder.Entity<ChartOfAccount>()
                .HasOne<ChartOfAccount>()
                .WithMany(c => c.Children)
                .HasForeignKey(a => a.ParentAccountId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<ChartOfAccount>().HasData(new ChartOfAccount
            {
                Id = Guid.Parse("4094B656-E07B-4670-8DFD-D717D57B4F74"),
                AccountName = "Rashed",
                AccountCode = "1001",
                AccountType = "Cash",
                Description = "Cash Account",
                ParentAccountId = null
            });
        }






        }



    }

