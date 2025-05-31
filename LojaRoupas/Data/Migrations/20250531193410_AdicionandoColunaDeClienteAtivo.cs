using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaRoupas.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoColunaDeClienteAtivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Clientes");
        }
    }
}
