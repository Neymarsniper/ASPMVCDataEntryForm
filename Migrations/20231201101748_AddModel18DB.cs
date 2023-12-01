using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberDataEntryForm.Migrations
{
    /// <inheritdoc />
    public partial class AddModel18DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserProposedDirectoryData_DataStatusId",
                table: "UserProposedDirectoryData",
                column: "DataStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProposedDirectoryData_UserStatusDirectoryData_DataStatusId",
                table: "UserProposedDirectoryData",
                column: "DataStatusId",
                principalTable: "UserStatusDirectoryData",
                principalColumn: "StatusCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProposedDirectoryData_UserStatusDirectoryData_DataStatusId",
                table: "UserProposedDirectoryData");

            migrationBuilder.DropIndex(
                name: "IX_UserProposedDirectoryData_DataStatusId",
                table: "UserProposedDirectoryData");
        }
    }
}
