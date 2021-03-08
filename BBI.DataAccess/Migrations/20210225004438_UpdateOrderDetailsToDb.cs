using Microsoft.EntityFrameworkCore.Migrations;

namespace BBI.DataAccess.Migrations
{
    public partial class UpdateOrderDetailsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "OrderDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_CityId",
                table: "OrderDetails",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Cities_CityId",
                table: "OrderDetails",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Cities_CityId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_CityId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "OrderDetails");
        }
    }
}
