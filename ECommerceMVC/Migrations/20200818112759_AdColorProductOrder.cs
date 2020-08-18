using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerceMVC.Migrations
{
    public partial class AdColorProductOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "color",
                table: "Product-Order",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "color",
                table: "Product-Order");
        }
    }
}
