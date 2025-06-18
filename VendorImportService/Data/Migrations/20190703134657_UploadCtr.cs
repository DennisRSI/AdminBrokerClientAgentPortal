using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VendorImport.Service.data.Migrations
{
    public partial class UploadCtr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "MerchantInventoryReservations",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldRowVersion: true);*/

            migrationBuilder.AddColumn<int>(
                name: "UploadCounter",
                table: "MerchantInventoryReservations",
                nullable: false,
                defaultValue: 0);

            /*migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "InventoryReservations",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldRowVersion: true);*/

            migrationBuilder.AddColumn<int>(
                name: "UploadCounter",
                table: "InventoryReservations",
                nullable: false,
                defaultValue: 0);

           /* migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "Adjustments",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldRowVersion: true);*/

            migrationBuilder.AddColumn<int>(
                name: "UploadCounter",
                table: "Adjustments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadCounter",
                table: "MerchantInventoryReservations");

            migrationBuilder.DropColumn(
                name: "UploadCounter",
                table: "InventoryReservations");

            migrationBuilder.DropColumn(
                name: "UploadCounter",
                table: "Adjustments");

            /*migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "MerchantInventoryReservations",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "InventoryReservations",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "Adjustments",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldRowVersion: true,
                oldNullable: true);*/
        }
    }
}
