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
        }



    }
}
