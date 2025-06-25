using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniAccountManagement.Web.Domain.Entities;
using MiniAccountManagement.Web.Domain.Repositories;

namespace MiniAccountManagement.Web.Infrastructure.Repositories
{
    public class VoucherRepository:IVoucherRepository
    {

        private readonly ApplicationDbContext _context;

        public VoucherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveVoucherAsync(Voucher voucher)
        {
            var entriesTable = new DataTable();
            entriesTable.Columns.Add("Id", typeof(Guid));
            entriesTable.Columns.Add("VoucherId", typeof(Guid));
            entriesTable.Columns.Add("AccountName", typeof(string));
            entriesTable.Columns.Add("Debit", typeof(decimal));
            entriesTable.Columns.Add("Credit", typeof(decimal));

            foreach (var entry in voucher.Entries)
            {
                entriesTable.Rows.Add(
                    entry.Id == Guid.Empty ? Guid.NewGuid() : entry.Id,
                    voucher.Id,
                    entry.AccountName,
                    entry.Debit,
                    entry.Credit
                );
            }

            var idParam = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = voucher.Id == Guid.Empty ? Guid.NewGuid() : voucher.Id };
            var dateParam = new SqlParameter("@Date", SqlDbType.Date) { Value = voucher.Date };
            var referenceNoParam = new SqlParameter("@ReferenceNo", SqlDbType.NVarChar, 50) { Value = voucher.ReferenceNo ?? (object)DBNull.Value };
            var typeParam = new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = voucher.Type ?? (object)DBNull.Value };
            var entriesParam = new SqlParameter("@Entries", SqlDbType.Structured)
            {
                TypeName = "dbo.VoucherEntryType",
                Value = entriesTable
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_SaveVoucher @Id, @Date, @ReferenceNo, @Type, @Entries",
                idParam, dateParam, referenceNoParam, typeParam, entriesParam);
        }
    }
}
