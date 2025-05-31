using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaRoupas.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoModelDeContrato2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroContrato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Produto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioImportador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataImportacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contratos");
        }
    }
}
