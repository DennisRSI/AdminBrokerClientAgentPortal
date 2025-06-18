using Microsoft.EntityFrameworkCore.Migrations;

namespace VendorImport.Service.data.Migrations
{
    public partial class ReservationIdChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReservationId",
                table: "MerchantInventoryReservations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ReservationId",
                table: "InventoryReservations",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "MerchantInventoryReservations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "InventoryReservations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
