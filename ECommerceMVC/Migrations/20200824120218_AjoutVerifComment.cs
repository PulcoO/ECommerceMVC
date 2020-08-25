using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerceMVC.Migrations
{
    public partial class AjoutVerifComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Verifed",
                table: "Comment",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verifed",
                table: "Comment");
        }
    }
}
