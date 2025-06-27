using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniAccountManagement.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModulePermissionProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = """"
                    Create or ALTER PROCEDURE [dbo].[sp_UpdateModulePermission]
                    @Id UNIQUEIDENTIFIER,
                    @RoleId NVARCHAR(MAX),
                    @ModuleName NVARCHAR(MAX),
                    @CanView BIT,
                    @CanCreate BIT,
                    @CanEdit BIT,
                    @CanDelete BIT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    UPDATE ModulePermissions SET
                        RoleId = @RoleId,
                        ModuleName = @ModuleName,
                        CanView = @CanView,
                        CanCreate = @CanCreate,
                        CanEdit = @CanEdit,
                        CanDelete = @CanDelete
                    WHERE Id = @Id;
                END


                """";
            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROCEDURE [dbo].[sp_UpdateModulePermission]";
            migrationBuilder.Sql(sql);

        }
    }
}
