using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirMastera.Infrastructure.Data.Migrations
{
    public partial class UpdateRepairTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE public.\"Repairs\" ALTER COLUMN \"Price\" TYPE numeric USING \"Price\"::numeric;");
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Repairs",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Repairs",
                type: "text",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
