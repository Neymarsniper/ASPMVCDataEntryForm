using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberDataEntryForm.Migrations
{
    /// <inheritdoc />
    public partial class AddModel11DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userType",
                table: "UserDirectoryData");

            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "UserDirectoryData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserDirectoryData_UserRoleId",
                table: "UserDirectoryData",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDirectoryData_GetUserTypes_UserRoleId",
                table: "UserDirectoryData",
                column: "UserRoleId",
                principalTable: "GetUserTypes",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDirectoryData_GetUserTypes_UserRoleId",
                table: "UserDirectoryData");

            migrationBuilder.DropIndex(
                name: "IX_UserDirectoryData_UserRoleId",
                table: "UserDirectoryData");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "UserDirectoryData");

            migrationBuilder.AddColumn<string>(
                name: "userType",
                table: "UserDirectoryData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
