using Microsoft.EntityFrameworkCore.Migrations;

namespace VendorImport.Service.data.Migrations
{
    public partial class Mispellings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubAffialiateCommission",
                table: "MerchantInventoryReservations",
                newName: "SubAffiliateCommission");

            migrationBuilder.RenameColumn(
                name: "Propertry",
                table: "MerchantInventoryReservations",
                newName: "Property");

            migrationBuilder.RenameColumn(
                name: "AffialiateCommissionPercentage",
                table: "MerchantInventoryReservations",
                newName: "AffiliteCommissionPercentage");

            migrationBuilder.RenameColumn(
                name: "SubAffialiateCommission",
                table: "InventoryReservations",
                newName: "SubAffiliateCommission");

            migrationBuilder.RenameColumn(
                name: "Propertry",
                table: "InventoryReservations",
                newName: "Property");

            migrationBuilder.RenameColumn(
                name: "Propertry",
                table: "Adjustments",
                newName: "Property");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubAffiliateCommission",
                table: "MerchantInventoryReservations",
                newName: "SubAffialiateCommission");

            migrationBuilder.RenameColumn(
                name: "Property",
                table: "MerchantInventoryReservations",
                newName: "Propertry");

            migrationBuilder.RenameColumn(
                name: "AffiliteCommissionPercentage",
                table: "MerchantInventoryReservations",
                newName: "AffialiateCommissionPercentage");

            migrationBuilder.RenameColumn(
                name: "SubAffiliateCommission",
                table: "InventoryReservations",
                newName: "SubAffialiateCommission");

            migrationBuilder.RenameColumn(
                name: "Property",
                table: "InventoryReservations",
                newName: "Propertry");

            migrationBuilder.RenameColumn(
                name: "Property",
                table: "Adjustments",
                newName: "Propertry");
        }
    }
}
