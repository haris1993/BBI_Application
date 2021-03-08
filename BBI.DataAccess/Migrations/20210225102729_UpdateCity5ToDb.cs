using Microsoft.EntityFrameworkCore.Migrations;

namespace BBI.DataAccess.Migrations
{
    public partial class UpdateCity5ToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<double>(
                name: "Tax",
                table: "Cities",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Tax",
                table: "Cities",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));

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
    }
}
