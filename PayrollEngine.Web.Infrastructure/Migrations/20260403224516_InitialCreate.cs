using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scenarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    SalaryType = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    DisabilityDegree = table.Column<int>(type: "INTEGER", nullable: false),
                    PayType = table.Column<int>(type: "INTEGER", nullable: false),
                    Sector = table.Column<int>(type: "INTEGER", nullable: false),
                    IncentiveType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenarios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scenarios");
        }
    }
}
