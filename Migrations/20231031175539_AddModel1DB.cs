using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberDataEntryForm.Migrations
{
    /// <inheritdoc />
    public partial class AddModel1DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDirectoryData_GetUserTypes_RoleId",
                table: "UserDirectoryData");

            migrationBuilder.DropIndex(
                name: "IX_UserDirectoryData_RoleId",
                table: "UserDirectoryData");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UserDirectoryData");

            migrationBuilder.AddColumn<string>(
                name: "userType",
                table: "UserDirectoryData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userType",
                table: "UserDirectoryData");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "UserDirectoryData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserDirectoryData_RoleId",
                table: "UserDirectoryData",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDirectoryData_GetUserTypes_RoleId",
                table: "UserDirectoryData",
                column: "RoleId",
                principalTable: "GetUserTypes",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
