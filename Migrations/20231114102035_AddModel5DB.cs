using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberDataEntryForm.Migrations
{
    /// <inheritdoc />
    public partial class AddModel5DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberFamilyDirectoryData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Relation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HomeAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ChildName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MemNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberFamilyDirectoryData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberFamilyDirectoryData_MemberDirectoryData_MemNo",
                        column: x => x.MemNo,
                        principalTable: "MemberDirectoryData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberFamilyDirectoryData_MemNo",
                table: "MemberFamilyDirectoryData",
                column: "MemNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberFamilyDirectoryData");
        }
    }
}
