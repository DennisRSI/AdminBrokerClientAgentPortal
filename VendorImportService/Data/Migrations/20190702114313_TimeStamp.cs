using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VendorImport.Service.data.Migrations
{
    public partial class TimeStamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "MerchantInventoryReservations",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "InventoryReservations",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Adjustments",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "MerchantInventoryReservations");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "InventoryReservations");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Adjustments");
        }
    }
}
