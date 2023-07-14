using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirMastera.Infrastructure.Data.Migrations
{
    public partial class AddInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Model = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: false),
                    PersonDbId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Persons_PersonDbId",
                        column: x => x.PersonDbId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PartName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    PartType = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Price = table.Column<string>(type: "text", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AppointmentTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CarDbId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repairs_Cars_CarDbId",
                        column: x => x.CarDbId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_Id",
                table: "Cars",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_Number",
                table: "Cars",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PersonDbId",
                table: "Cars",
                column: "PersonDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Id",
                table: "Persons",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Phone",
                table: "Persons",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_CarDbId",
                table: "Repairs",
                column: "CarDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_Id",
                table: "Repairs",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
