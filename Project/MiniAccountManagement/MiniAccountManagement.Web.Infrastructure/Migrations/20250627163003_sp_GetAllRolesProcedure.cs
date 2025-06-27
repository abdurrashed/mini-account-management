using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniAccountManagement.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sp_GetAllRolesProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = """"
                 Create or ALTER PROCEDURE sp_GetAllRoles
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT Id, Name FROM AspNetRoles;
                END
                """";
            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROCEDURE [dbo].[sp_GetAllRoles]";
            migrationBuilder.Sql(sql);

        }
    }
}
