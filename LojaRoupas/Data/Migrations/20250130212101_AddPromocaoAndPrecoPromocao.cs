using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaRoupas.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPromocaoAndPrecoPromocao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecoPromocao",
                table: "Roupas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Promocao",
                table: "Roupas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoPromocao",
                table: "Roupas");

            migrationBuilder.DropColumn(
                name: "Promocao",
                table: "Roupas");
        }
    }
}
