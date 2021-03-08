using Microsoft.EntityFrameworkCore.Migrations;

namespace BBI.DataAccess.Migrations
{
    public partial class AddUpdatePackageToCategoryPackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicePackages_Packages_PackageId",
                table: "ServicePackages");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.CreateTable(
                name: "CategoryPackages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    DisplayPackage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPackages", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ServicePackages_CategoryPackages_PackageId",
                table: "ServicePackages",
                column: "PackageId",
                principalTable: "CategoryPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicePackages_CategoryPackages_PackageId",
                table: "ServicePackages");

            migrationBuilder.DropTable(
                name: "CategoryPackages");

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ServicePackages_Packages_PackageId",
                table: "ServicePackages",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
