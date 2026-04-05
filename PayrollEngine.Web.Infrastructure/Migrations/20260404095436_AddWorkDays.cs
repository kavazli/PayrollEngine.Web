using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkDays",
                table: "PayrollMonths",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkDays",
                table: "PayrollMonths");
        }
    }
}
