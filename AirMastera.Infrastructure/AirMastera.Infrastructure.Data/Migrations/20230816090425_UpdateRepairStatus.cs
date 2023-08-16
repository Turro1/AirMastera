using AirMastera.Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirMastera.Infrastructure.Data.Migrations
{
    public partial class UpdateRepairStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<RepairStatus>(
                name: "repair_status",
                table: "repair",
                type: "repair_status",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "repair_status",
                table: "repair",
                type: "integer",
                nullable: false,
                oldClrType: typeof(RepairStatus),
                oldType: "repair_status");
        }
    }
}
