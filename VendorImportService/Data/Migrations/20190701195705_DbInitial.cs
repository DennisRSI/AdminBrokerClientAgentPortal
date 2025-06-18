using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VendorImport.Service.data.Migrations
{
    public partial class DbInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adjustments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Confirmation = table.Column<string>(maxLength: 100, nullable: false),
                    Propertry = table.Column<string>(maxLength: 500, nullable: false),
                    GuestFirstName = table.Column<string>(maxLength: 100, nullable: false),
                    GuestLastName = table.Column<string>(maxLength: 255, nullable: false),
                    CommissionAdjustment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adjustments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Confirmation = table.Column<string>(maxLength: 100, nullable: false),
                    Propertry = table.Column<string>(maxLength: 500, nullable: false),
                    GuestFirstName = table.Column<string>(maxLength: 100, nullable: false),
                    GuestLastName = table.Column<string>(maxLength: 255, nullable: false),
                    AffiliateName = table.Column<string>(maxLength: 500, nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    SiteName = table.Column<string>(maxLength: 500, nullable: false),
                    CheckoutDate = table.Column<DateTime>(nullable: false),
                    BookedDate = table.Column<DateTime>(nullable: false),
                    ReservationStatus = table.Column<string>(maxLength: 50, nullable: false),
                    RoomNights = table.Column<int>(nullable: false),
                    FlatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RoomRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ARNCallCenterFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCommission = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubAffialiateCommission = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCommissionAfterSubAffiliate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReservationId = table.Column<int>(nullable: false),
                    RegistrationId = table.Column<string>(maxLength: 1000, nullable: true),
                    RegistrationName = table.Column<string>(maxLength: 1000, nullable: true),
                    BookedRoomRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommissionReceived = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CollectionExpense = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryReservations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MerchantInventoryReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Confirmation = table.Column<string>(maxLength: 100, nullable: false),
                    Propertry = table.Column<string>(maxLength: 500, nullable: false),
                    GuestFirstName = table.Column<string>(maxLength: 100, nullable: false),
                    GuestLastName = table.Column<string>(maxLength: 255, nullable: false),
                    AffiliateName = table.Column<string>(maxLength: 500, nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    SiteName = table.Column<string>(maxLength: 500, nullable: false),
                    CheckoutDate = table.Column<DateTime>(nullable: false),
                    BookedDate = table.Column<DateTime>(nullable: false),
                    ReservationStatus = table.Column<string>(maxLength: 50, nullable: false),
                    RoomNights = table.Column<int>(nullable: false),
                    FlatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RoomRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ARNCallCenterFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCommission = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubAffialiateCommission = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCommissionAfterSubAffiliate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReservationId = table.Column<int>(nullable: false),
                    RegistrationId = table.Column<string>(maxLength: 1000, nullable: true),
                    RegistrationName = table.Column<string>(maxLength: 1000, nullable: true),
                    GrossSaleWithTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostOfHotelWithTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CardProcessingFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AffialiateCommissionPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ARNTransactionFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantInventoryReservations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adjustments_Confirmation",
                table: "Adjustments",
                column: "Confirmation");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReservations_Confirmation_SiteId",
                table: "InventoryReservations",
                columns: new[] { "Confirmation", "SiteId" });

            migrationBuilder.CreateIndex(
                name: "IX_MerchantInventoryReservations_Confirmation_SiteId",
                table: "MerchantInventoryReservations",
                columns: new[] { "Confirmation", "SiteId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adjustments");

            migrationBuilder.DropTable(
                name: "InventoryReservations");

            migrationBuilder.DropTable(
                name: "MerchantInventoryReservations");
        }
    }
}
