using System;
using AirMastera.Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirMastera.Infrastructure.Data.Migrations
{
    public partial class AddInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:repair_status", "created,work_in_process,failed,finished");

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fullname = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "car",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    model = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    number = table.Column<string>(type: "text", nullable: false),
                    avatar = table.Column<string>(type: "text", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car", x => x.id);
                    table.ForeignKey(
                        name: "FK_car_person_person_id",
                        column: x => x.person_id,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "repair",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    part_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    part_type = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    repair_status = table.Column<RepairStatus>(type: "repair_status", nullable: false),
                    appointment_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    car_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_repair", x => x.id);
                    table.ForeignKey(
                        name: "FK_repair_car_car_id",
                        column: x => x.car_id,
                        principalTable: "car",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_car_id",
                table: "car",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_car_number",
                table: "car",
                column: "number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_car_person_id",
                table: "car",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_id",
                table: "person",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_person_phone",
                table: "person",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_repair_car_id",
                table: "repair",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "IX_repair_id",
                table: "repair",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "repair");

            migrationBuilder.DropTable(
                name: "car");

            migrationBuilder.DropTable(
                name: "person");
        }
    }
}
