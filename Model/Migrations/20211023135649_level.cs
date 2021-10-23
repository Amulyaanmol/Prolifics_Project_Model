using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.OData.Edm;

namespace Model.Migrations
{
    public partial class level : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectEf",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                       ,
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenDate = table.Column<Date>(type: "date", nullable: false),
                    CloseDate = table.Column<Date>(type: "date", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEf", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "RoleEf",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                       ,
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleEf", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEf",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                       ,
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeRoleId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEf", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_EmployeeEf_ProjectEf_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectEf",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEf_ProjectId",
                table: "EmployeeEf",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeEf");

            migrationBuilder.DropTable(
                name: "RoleEf");

            migrationBuilder.DropTable(
                name: "ProjectEf");
        }
    }
}
