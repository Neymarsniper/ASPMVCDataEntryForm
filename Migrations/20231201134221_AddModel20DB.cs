using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberDataEntryForm.Migrations
{
    /// <inheritdoc />
    public partial class AddModel20DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserProposedDirectoryData_UserId",
                table: "UserProposedDirectoryData",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProposedDirectoryData_UserDirectoryData_UserId",
                table: "UserProposedDirectoryData",
                column: "UserId",
                principalTable: "UserDirectoryData",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProposedDirectoryData_UserDirectoryData_UserId",
                table: "UserProposedDirectoryData");

            migrationBuilder.DropIndex(
                name: "IX_UserProposedDirectoryData_UserId",
                table: "UserProposedDirectoryData");
        }
    }
}
