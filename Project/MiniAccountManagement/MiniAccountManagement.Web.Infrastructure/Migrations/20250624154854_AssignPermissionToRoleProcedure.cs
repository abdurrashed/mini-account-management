using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniAccountManagement.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AssignPermissionToRoleProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = """"

                CREATE OR ALTER PROCEDURE sp_AssignPermissionToRole
                    @RoleId NVARCHAR(450),
                    @ModuleName NVARCHAR(100),
                    @CanView BIT,
                    @CanCreate BIT,
                    @CanEdit BIT,
                    @CanDelete BIT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    INSERT INTO ModulePermissions (Id, RoleId, ModuleName, CanView, CanCreate, CanEdit, CanDelete)
                    VALUES (NEWID(), @RoleId, @ModuleName, @CanView, @CanCreate, @CanEdit, @CanDelete);
                END



                """";

            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


            var sql = "DROP PROCEDURE [dbo].[sp_AssignPermissionToRole]";
            migrationBuilder.Sql(sql);
        }
    }
}
