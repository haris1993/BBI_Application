using Microsoft.EntityFrameworkCore.Migrations;

namespace BBI.DataAccess.Migrations
{
    public partial class UpdateCity4ToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tax",
                table: "Cities",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tax",
                table: "Cities");
        }
    }
}
