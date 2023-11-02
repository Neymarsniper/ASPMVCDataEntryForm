using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberDataEntryForm.Migrations
{
    /// <inheritdoc />
    public partial class DataEntryDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberDirectoryData",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MemNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternateMobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateofMarriage = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NameofSpouse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpouseDob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChildName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    //UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberDirectoryData", x => x.Name);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberDirectoryData");
        }
    }
}
