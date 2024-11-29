using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyPortfolio.Migrations
{
    /// <inheritdoc />
    public partial class UserInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spese");

            migrationBuilder.DropTable(
                name: "SottoCategorie");

            migrationBuilder.DropTable(
                name: "Categorie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SottoCategorie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoriaId = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SottoCategorie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SottoCategorie_Categorie_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spese",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo1Id = table.Column<int>(type: "integer", nullable: false),
                    Tipo2Id = table.Column<int>(type: "integer", nullable: false),
                    Costo = table.Column<decimal>(type: "numeric", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Descrizione = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spese", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spese_Categorie_Tipo1Id",
                        column: x => x.Tipo1Id,
                        principalTable: "Categorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spese_SottoCategorie_Tipo2Id",
                        column: x => x.Tipo2Id,
                        principalTable: "SottoCategorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SottoCategorie_CategoriaId",
                table: "SottoCategorie",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Spese_Tipo1Id",
                table: "Spese",
                column: "Tipo1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Spese_Tipo2Id",
                table: "Spese",
                column: "Tipo2Id");
        }
    }
}
