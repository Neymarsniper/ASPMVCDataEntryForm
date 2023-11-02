using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberDataEntryForm.Migrations
{
    /// <inheritdoc />
    public partial class AddNewModelDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberDirectoryData",
                table: "MemberDirectoryData");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MemberDirectoryData",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MemberDirectoryData",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberDirectoryData",
                table: "MemberDirectoryData",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GetUserTypes",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetUserTypes", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "UserDirectoryData",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordConfirmed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDirectoryData", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserDirectoryData_GetUserTypes_RoleId",
                        column: x => x.RoleId,
                        principalTable: "GetUserTypes",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDirectoryData_RoleId",
                table: "UserDirectoryData",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDirectoryData");

            migrationBuilder.DropTable(
                name: "GetUserTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberDirectoryData",
                table: "MemberDirectoryData");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MemberDirectoryData");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MemberDirectoryData",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberDirectoryData",
                table: "MemberDirectoryData",
                column: "Name");
        }
    }
}
