using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddParamsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisabilityDegrees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Degree = table.Column<int>(type: "INTEGER", nullable: true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisabilityDegrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeTaxBrackets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Bracket = table.Column<int>(type: "INTEGER", nullable: false),
                    MinAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Rate = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeTaxBrackets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayrollResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkDays = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    Overtime_50_Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Overtime_100_Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Bonus = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    SSContributionBase = table.Column<decimal>(type: "TEXT", nullable: false),
                    EmployeeSSContributionAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    EmployeeUIContributionAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    IncomeTaxBase = table.Column<decimal>(type: "TEXT", nullable: false),
                    CumulativeIncomeTaxBase = table.Column<decimal>(type: "TEXT", nullable: false),
                    IncomeTax = table.Column<decimal>(type: "TEXT", nullable: false),
                    IncomeTaxRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    IncomeTaxExemption = table.Column<decimal>(type: "TEXT", nullable: false),
                    StampTax = table.Column<decimal>(type: "TEXT", nullable: false),
                    StampTaxExemption = table.Column<decimal>(type: "TEXT", nullable: false),
                    NetSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    ShoppingVoucherNet = table.Column<decimal>(type: "TEXT", nullable: false),
                    ShoppingVoucherIncomeTax = table.Column<decimal>(type: "TEXT", nullable: false),
                    ShoppingVoucherStampTax = table.Column<decimal>(type: "TEXT", nullable: false),
                    ShoppingVoucherGrossAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    EmployerSSContributionAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    EmployerUIContributionAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalEmployerCost = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SSCeilings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Ceiling = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSCeilings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SSParams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    ActiveEmployeeSSRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    ActiveEmployeeUIRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    ActiveEmployerSSRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    ActiveEmployerUIRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    RetiredEmployeeSSRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    RetiredEmployerSSRate = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSParams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StampTaxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Rate = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StampTaxes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisabilityDegrees");

            migrationBuilder.DropTable(
                name: "IncomeTaxBrackets");

            migrationBuilder.DropTable(
                name: "PayrollResults");

            migrationBuilder.DropTable(
                name: "SSCeilings");

            migrationBuilder.DropTable(
                name: "SSParams");

            migrationBuilder.DropTable(
                name: "StampTaxes");
        }
    }
}
