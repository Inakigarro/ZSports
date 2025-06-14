using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZSports.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCanchas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cancha",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    TipoSuelo = table.Column<int>(type: "int", nullable: false),
                    EstablecimientoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cancha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cancha_Establecimiento_EstablecimientoId",
                        column: x => x.EstablecimientoId,
                        principalTable: "Establecimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cancha_EstablecimientoId",
                table: "Cancha",
                column: "EstablecimientoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cancha");
        }
    }
}
