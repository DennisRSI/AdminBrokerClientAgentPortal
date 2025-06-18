using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes1.Service.Data.Migrations
{
    public partial class DatesAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BrokerPaidDate",
                table: "BookingCommissions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecordChangeDate",
                table: "BookingCommissions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecordChangeRSIId",
                table: "BookingCommissions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VendorPaidDate",
                table: "BookingCommissions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrokerPaidDate",
                table: "BookingCommissions");

            migrationBuilder.DropColumn(
                name: "RecordChangeDate",
                table: "BookingCommissions");

            migrationBuilder.DropColumn(
                name: "RecordChangeRSIId",
                table: "BookingCommissions");

            migrationBuilder.DropColumn(
                name: "VendorPaidDate",
                table: "BookingCommissions");
        }
    }
}
