using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMinimumWageYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "MinimumWages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "MinimumWages");
        }
    }
}
