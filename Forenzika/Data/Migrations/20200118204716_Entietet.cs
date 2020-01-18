using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Forenzika.Data.Migrations
{
    public partial class Entietet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emertimi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(nullable: true),
                    Mbiemri = table.Column<string>(nullable: true),
                    NumriPersonal = table.Column<int>(nullable: false),
                    Adresa = table.Column<string>(nullable: true),
                    DataLindjes = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rasti",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmriRastit = table.Column<string>(nullable: true),
                    Vendi = table.Column<string>(nullable: true),
                    Pershkrimi = table.Column<string>(nullable: true),
                    ViktimiId = table.Column<int>(nullable: true),
                    IAkuzuariId = table.Column<int>(nullable: true),
                    KategoriaId = table.Column<int>(nullable: true),
                    HetuesiId = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rasti", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rasti_AspNetUsers_HetuesiId",
                        column: x => x.HetuesiId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rasti_Personi_IAkuzuariId",
                        column: x => x.IAkuzuariId,
                        principalTable: "Personi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rasti_Kategoria_KategoriaId",
                        column: x => x.KategoriaId,
                        principalTable: "Kategoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rasti_Personi_ViktimiId",
                        column: x => x.ViktimiId,
                        principalTable: "Personi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Foto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pathi = table.Column<string>(nullable: true),
                    RastiID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foto_Rasti_RastiID",
                        column: x => x.RastiID,
                        principalTable: "Rasti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foto_RastiID",
                table: "Foto",
                column: "RastiID");

            migrationBuilder.CreateIndex(
                name: "IX_Rasti_HetuesiId",
                table: "Rasti",
                column: "HetuesiId");

            migrationBuilder.CreateIndex(
                name: "IX_Rasti_IAkuzuariId",
                table: "Rasti",
                column: "IAkuzuariId");

            migrationBuilder.CreateIndex(
                name: "IX_Rasti_KategoriaId",
                table: "Rasti",
                column: "KategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rasti_ViktimiId",
                table: "Rasti",
                column: "ViktimiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foto");

            migrationBuilder.DropTable(
                name: "Rasti");

            migrationBuilder.DropTable(
                name: "Personi");

            migrationBuilder.DropTable(
                name: "Kategoria");
        }
    }
}
