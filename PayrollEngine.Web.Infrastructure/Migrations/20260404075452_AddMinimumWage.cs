using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMinimumWage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MinimumWages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    NetSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    RetiredNetSalary = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinimumWages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayrollMonths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    Overtime_50_Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Overtime_100_Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Bonus = table.Column<decimal>(type: "TEXT", nullable: false),
                    ShoppingVoucher = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollMonths", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MinimumWages");

            migrationBuilder.DropTable(
                name: "PayrollMonths");
        }
    }
}
