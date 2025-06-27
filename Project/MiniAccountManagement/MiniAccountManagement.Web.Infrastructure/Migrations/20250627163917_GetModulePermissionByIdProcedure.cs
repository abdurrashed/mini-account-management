using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniAccountManagement.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GetModulePermissionByIdProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = """"

                 CREATE OR  ALTER PROCEDURE sp_GetModulePermissionById
                    @Id UNIQUEIDENTIFIER
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT Id, RoleId, ModuleName, CanView, CanCreate, CanEdit, CanDelete
                    FROM ModulePermissions WHERE Id = @Id;
                END
                """";
            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            var sql = "DROP PROCEDURE [dbo].[sp_GetModulePermissionById]";
            migrationBuilder.Sql(sql);
        }
    }
}
