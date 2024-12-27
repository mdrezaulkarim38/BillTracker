using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillTracker.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureBillAmountPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequestForDeletion",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestForDeletion",
                table: "Products");
        }
    }
}
