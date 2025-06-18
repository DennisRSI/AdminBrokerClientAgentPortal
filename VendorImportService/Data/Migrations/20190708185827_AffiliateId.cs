using Microsoft.EntityFrameworkCore.Migrations;

namespace VendorImport.Service.data.Migrations
{
    public partial class AffiliateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AffiliateId",
                table: "MerchantInventoryReservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AffiliateId",
                table: "InventoryReservations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AffiliateId",
                table: "MerchantInventoryReservations");

            migrationBuilder.DropColumn(
                name: "AffiliateId",
                table: "InventoryReservations");
        }
    }
}
