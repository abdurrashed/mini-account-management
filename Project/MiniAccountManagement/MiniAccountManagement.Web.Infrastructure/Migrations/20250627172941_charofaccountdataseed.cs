using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniAccountManagement.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class charofaccountdataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ChartOfAccounts",
                columns: new[] { "Id", "AccountCode", "AccountName", "AccountType", "Description", "ParentAccountId" },
                values: new object[] { new Guid("4094b656-e07b-4670-8dfd-d717d57b4f74"), "1001", "Rashed", "Cash", "Cash Account", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChartOfAccounts",
                keyColumn: "Id",
                keyValue: new Guid("4094b656-e07b-4670-8dfd-d717d57b4f74"));
        }
    }
}
