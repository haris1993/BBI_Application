using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BBI.DataAccess.Migrations
{
    public partial class AddUpdateOrderHeaderToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "City",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "OrderHeaders");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "OrderHeaders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "OrderHeaders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "OrderHeaders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "OrderHeaders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JMBG",
                table: "OrderHeaders",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "OrderHeaders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_CityId",
                table: "OrderHeaders",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_Cities_CityId",
                table: "OrderHeaders",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_Cities_CityId",
                table: "OrderHeaders");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeaders_CityId",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "JMBG",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "OrderHeaders");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
