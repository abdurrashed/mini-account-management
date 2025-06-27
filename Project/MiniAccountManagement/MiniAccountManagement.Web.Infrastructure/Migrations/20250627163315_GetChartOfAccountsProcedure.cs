using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniAccountManagement.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GetChartOfAccountsProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = """"

                  CREATE OR ALTER PROCEDURE sp_GetChartOfAccounts
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT Id, AccountCode, AccountName, ParentAccountId, AccountType, Description FROM ChartOfAccounts ORDER BY AccountCode;
                END
                

                """";
            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROCEDURE [dbo].[sp_GetChartOfAccounts]";
            migrationBuilder.Sql(sql);

        }
    }
}
