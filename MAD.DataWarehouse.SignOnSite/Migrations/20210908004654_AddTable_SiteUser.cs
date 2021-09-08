using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAD.DataWarehouse.SignOnSite.Migrations
{
    public partial class AddTable_SiteUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Induction_Id = table.Column<int>(type: "int", nullable: true),
                    Induction_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Induction_Number = table.Column<int>(type: "int", nullable: true),
                    Induction_State_AsString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Induction_State_SetAt = table.Column<DateTime>(type: "date", nullable: true),
                    Induction_State_SetBy_Id = table.Column<int>(type: "int", nullable: true),
                    Induction_State_SetBy_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Induction_State_SetBy_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteCompany_Id = table.Column<int>(type: "int", nullable: true),
                    SiteCompany_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteUser", x => new { x.Id, x.SiteId });
                    table.ForeignKey(
                        name: "FK_SiteUser_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteUser_SiteId",
                table: "SiteUser",
                column: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteUser");
        }
    }
}
