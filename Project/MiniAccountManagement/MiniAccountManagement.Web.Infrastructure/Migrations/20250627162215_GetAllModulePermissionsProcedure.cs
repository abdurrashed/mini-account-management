using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniAccountManagement.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GetAllModulePermissionsProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = """"

                Create or ALTER PROCEDURE sp_GetAllModulePermissions
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT Id, RoleId, ModuleName, CanView, CanCreate, CanEdit, CanDelete
                    FROM ModulePermissions;
                END
                """";
            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROCEDURE [dbo].[sp_GetAllModulePermissions]";
            migrationBuilder.Sql(sql);
        }
    }
}
