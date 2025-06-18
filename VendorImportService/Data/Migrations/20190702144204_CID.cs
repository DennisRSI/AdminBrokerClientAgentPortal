using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VendorImport.Service.data.Migrations
{
    public partial class CID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "MerchantInventoryReservations",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldRowVersion: true,
                oldNullable: true);*/

            migrationBuilder.AddColumn<string>(
                name: "CID",
                table: "MerchantInventoryReservations",
                nullable: true);

            /*migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "InventoryReservations",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldRowVersion: true,
                oldNullable: true);*/

            migrationBuilder.AddColumn<string>(
                name: "CID",
                table: "InventoryReservations",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ComissionProcessingFee",
                table: "InventoryReservations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            /*migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "Adjustments",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldRowVersion: true,
                oldNullable: true);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CID",
                table: "MerchantInventoryReservations");

            migrationBuilder.DropColumn(
                name: "CID",
                table: "InventoryReservations");

            migrationBuilder.DropColumn(
                name: "ComissionProcessingFee",
                table: "InventoryReservations");

            /*migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "MerchantInventoryReservations",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldRowVersion: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "InventoryReservations",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldRowVersion: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "Adjustments",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldRowVersion: true);*/
        }
    }
}
