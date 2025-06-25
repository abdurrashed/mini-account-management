using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniAccountManagement.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SaveVoucherProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create user-defined table type VoucherEntryType if not exists
            var createTypeSql = @"
                IF NOT EXISTS (SELECT * FROM sys.types WHERE is_table_type = 1 AND name = 'VoucherEntryType')
                BEGIN
                    CREATE TYPE dbo.VoucherEntryType AS TABLE
                    (
                        Id UNIQUEIDENTIFIER,
                        VoucherId UNIQUEIDENTIFIER,
                        AccountName NVARCHAR(255),
                        Debit DECIMAL(18,2),
                        Credit DECIMAL(18,2)
                    );
                END
            ";
            migrationBuilder.Sql(createTypeSql);

            // Create or alter stored procedure
            var createProcSql = @"
                CREATE OR ALTER PROCEDURE sp_SaveVoucher
                    @Id UNIQUEIDENTIFIER,
                    @Date DATE,
                    @ReferenceNo NVARCHAR(50),
                    @Type NVARCHAR(50),
                    @Entries dbo.VoucherEntryType READONLY
                AS
                BEGIN
                    SET NOCOUNT ON;

                    INSERT INTO Vouchers (Id, Date, ReferenceNo, Type)
                    VALUES (@Id, @Date, @ReferenceNo, @Type);

                    INSERT INTO VoucherEntries (Id, VoucherId, AccountName, Debit, Credit)
                    SELECT Id, VoucherId, AccountName, Debit, Credit FROM @Entries;
                END
            ";
            migrationBuilder.Sql(createProcSql);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop stored procedure first
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[sp_SaveVoucher];");

            // Drop user-defined table type
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.types WHERE is_table_type = 1 AND name = 'VoucherEntryType')
                BEGIN
                    DROP TYPE dbo.VoucherEntryType;
                END
            ");
        }
    }
}
