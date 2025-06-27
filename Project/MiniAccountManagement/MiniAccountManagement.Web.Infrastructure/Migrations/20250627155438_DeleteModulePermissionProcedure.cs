using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniAccountManagement.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteModulePermissionProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = """"
              CREATE OR ALTER PROCEDURE sp_DeleteModulePermission
              @Id UNIQUEIDENTIFIER
              AS
              BEGIN
              SET NOCOUNT ON;
               DELETE FROM ModulePermissions WHERE Id = @Id;
               END

              """";
            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            var sql = "DROP PROCEDURE [dbo].[sp_DeleteModulePermission]";
            migrationBuilder.Sql(sql);

        }
    }
}
