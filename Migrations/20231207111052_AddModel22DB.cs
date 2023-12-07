using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberDataEntryForm.Migrations
{
    /// <inheritdoc />
    public partial class AddModel22DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemAddressId",
                table: "MemberProposedDirectoryData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemBusinessId",
                table: "MemberProposedDirectoryData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemFamilyId",
                table: "MemberProposedDirectoryData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthId",
                table: "MemberFamilyDirectoryData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthId",
                table: "MemberBusinessDirectoryData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthId",
                table: "MemberAddressDirectoryData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberProposedDirectoryData_MemAddressId",
                table: "MemberProposedDirectoryData",
                column: "MemAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberProposedDirectoryData_MemBusinessId",
                table: "MemberProposedDirectoryData",
                column: "MemBusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberProposedDirectoryData_MemFamilyId",
                table: "MemberProposedDirectoryData",
                column: "MemFamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberProposedDirectoryData_MemberAddressDirectoryData_MemAddressId",
                table: "MemberProposedDirectoryData",
                column: "MemAddressId",
                principalTable: "MemberAddressDirectoryData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberProposedDirectoryData_MemberBusinessDirectoryData_MemBusinessId",
                table: "MemberProposedDirectoryData",
                column: "MemBusinessId",
                principalTable: "MemberBusinessDirectoryData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberProposedDirectoryData_MemberFamilyDirectoryData_MemFamilyId",
                table: "MemberProposedDirectoryData",
                column: "MemFamilyId",
                principalTable: "MemberFamilyDirectoryData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberProposedDirectoryData_MemberAddressDirectoryData_MemAddressId",
                table: "MemberProposedDirectoryData");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberProposedDirectoryData_MemberBusinessDirectoryData_MemBusinessId",
                table: "MemberProposedDirectoryData");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberProposedDirectoryData_MemberFamilyDirectoryData_MemFamilyId",
                table: "MemberProposedDirectoryData");

            migrationBuilder.DropIndex(
                name: "IX_MemberProposedDirectoryData_MemAddressId",
                table: "MemberProposedDirectoryData");

            migrationBuilder.DropIndex(
                name: "IX_MemberProposedDirectoryData_MemBusinessId",
                table: "MemberProposedDirectoryData");

            migrationBuilder.DropIndex(
                name: "IX_MemberProposedDirectoryData_MemFamilyId",
                table: "MemberProposedDirectoryData");

            migrationBuilder.DropColumn(
                name: "MemAddressId",
                table: "MemberProposedDirectoryData");

            migrationBuilder.DropColumn(
                name: "MemBusinessId",
                table: "MemberProposedDirectoryData");

            migrationBuilder.DropColumn(
                name: "MemFamilyId",
                table: "MemberProposedDirectoryData");

            migrationBuilder.DropColumn(
                name: "AuthId",
                table: "MemberFamilyDirectoryData");

            migrationBuilder.DropColumn(
                name: "AuthId",
                table: "MemberBusinessDirectoryData");

            migrationBuilder.DropColumn(
                name: "AuthId",
                table: "MemberAddressDirectoryData");
        }
    }
}
