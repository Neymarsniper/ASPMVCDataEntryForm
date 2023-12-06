using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberDataEntryForm.Migrations
{
    /// <inheritdoc />
    public partial class AddModel21DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthId",
                table: "MemberDirectoryData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MemberProposedDirectoryData",
                columns: table => new
                {
                    MemProId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternateMobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofMarriage = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NameofSpouse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseDob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChildName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditonalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyChildName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessPostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthId = table.Column<int>(type: "int", nullable: true),
                    MemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberProposedDirectoryData", x => x.MemProId);
                    table.ForeignKey(
                        name: "FK_MemberProposedDirectoryData_MemberDirectoryData_MemId",
                        column: x => x.MemId,
                        principalTable: "MemberDirectoryData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberProposedDirectoryData_MemId",
                table: "MemberProposedDirectoryData",
                column: "MemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberProposedDirectoryData");

            migrationBuilder.DropColumn(
                name: "AuthId",
                table: "MemberDirectoryData");
        }
    }
}
