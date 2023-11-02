using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberDataEntryForm.Migrations
{
    /// <inheritdoc />
    public partial class AddModelDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPassword",
                table: "MemberDirectoryData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserPassword",
                table: "MemberDirectoryData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
