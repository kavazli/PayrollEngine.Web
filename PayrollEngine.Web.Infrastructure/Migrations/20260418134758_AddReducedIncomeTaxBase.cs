using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReducedIncomeTaxBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ReducedIncomeTaxBase",
                table: "PayrollResults",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReducedIncomeTaxBase",
                table: "PayrollResults");
        }
    }
}
