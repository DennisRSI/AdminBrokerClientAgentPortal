using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes1.Service.Data.Migrations
{
    public partial class AdminNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminNote",
                table: "BookingCommissions",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BrokerPaidAmount",
                table: "BookingCommissions",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminNote",
                table: "BookingCommissions");

            migrationBuilder.DropColumn(
                name: "BrokerPaidAmount",
                table: "BookingCommissions");
        }
    }
}
