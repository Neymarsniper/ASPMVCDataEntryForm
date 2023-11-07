using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberDataEntryForm.Migrations
{
    /// <inheritdoc />
    public partial class AddModel4DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FatherMobileNo",
                table: "MemberFamilyDirectoryData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FatherOccupation",
                table: "MemberFamilyDirectoryData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MemNo",
                table: "MemberFamilyDirectoryData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MotherMobileNo",
                table: "MemberFamilyDirectoryData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MotherOccupation",
                table: "MemberFamilyDirectoryData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpouseMobileNo",
                table: "MemberFamilyDirectoryData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpouseOccupation",
                table: "MemberFamilyDirectoryData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FatherMobileNo",
                table: "MemberFamilyDirectoryData");

            migrationBuilder.DropColumn(
                name: "FatherOccupation",
                table: "MemberFamilyDirectoryData");

            migrationBuilder.DropColumn(
                name: "MemNo",
                table: "MemberFamilyDirectoryData");

            migrationBuilder.DropColumn(
                name: "MotherMobileNo",
                table: "MemberFamilyDirectoryData");

            migrationBuilder.DropColumn(
                name: "MotherOccupation",
                table: "MemberFamilyDirectoryData");

            migrationBuilder.DropColumn(
                name: "SpouseMobileNo",
                table: "MemberFamilyDirectoryData");

            migrationBuilder.DropColumn(
                name: "SpouseOccupation",
                table: "MemberFamilyDirectoryData");
        }
    }
}
