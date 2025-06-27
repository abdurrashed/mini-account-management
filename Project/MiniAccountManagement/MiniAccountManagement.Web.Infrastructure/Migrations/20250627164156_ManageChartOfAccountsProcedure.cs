using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniAccountManagement.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ManageChartOfAccountsProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = """"

                    CREATE OR ALTER PROCEDURE [dbo].[sp_ManageChartOfAccounts]
                    @Action NVARCHAR(10),
                    @Id UNIQUEIDENTIFIER = NULL,
                    @AccountName NVARCHAR(200) = NULL,
                    @AccountCode NVARCHAR(50) = NULL,
                    @ParentAccountId UNIQUEIDENTIFIER = NULL,
                    @AccountType NVARCHAR(50) = NULL,
                    @Description NVARCHAR(500) = NULL
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF @Action = 'Create'
                    BEGIN
                        INSERT INTO ChartOfAccounts (Id, AccountName, AccountCode, ParentAccountId, AccountType, Description)
                        VALUES (@Id, @AccountName, @AccountCode, @ParentAccountId, @AccountType, @Description);
                    END
                    ELSE IF @Action = 'Update'
                    BEGIN
                        UPDATE ChartOfAccounts SET
                            AccountName = @AccountName,
                            AccountCode = @AccountCode,
                            ParentAccountId = @ParentAccountId,
                            AccountType = @AccountType,
                            Description = @Description
                        WHERE Id = @Id;
                    END
                    ELSE IF @Action = 'Delete'
                    BEGIN
                        -- Unlink children first
                        UPDATE ChartOfAccounts
                        SET ParentAccountId = NULL
                        WHERE ParentAccountId = @Id;

                        -- Then delete the account
                        DELETE FROM ChartOfAccounts
                        WHERE Id = @Id;
                    END
                END

                """";
            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROCEDURE [dbo].[sp_ManageChartOfAccounts]";
            migrationBuilder.Sql(sql);

        }
    }
}
