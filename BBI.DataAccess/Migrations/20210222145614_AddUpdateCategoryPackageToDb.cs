using Microsoft.EntityFrameworkCore.Migrations;

namespace BBI.DataAccess.Migrations
{
    public partial class AddUpdateCategoryPackageToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayPackage",
                table: "CategoryPackages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayPackage",
                table: "CategoryPackages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
