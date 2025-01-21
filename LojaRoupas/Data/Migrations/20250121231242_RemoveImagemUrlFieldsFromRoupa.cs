using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaRoupas.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImagemUrlFieldsFromRoupa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemUrl2",
                table: "Roupas");

            migrationBuilder.DropColumn(
                name: "ImagemUrl3",
                table: "Roupas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl2",
                table: "Roupas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl3",
                table: "Roupas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
