using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaRoupas.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddClienteIdToAspNetUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
            name: "ClienteId",
            table: "AspNetUsers",
            nullable: true); 

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Clientes_ClienteId", 
                table: "AspNetUsers",
                column: "ClienteId",
                principalTable: "Clientes", 
                principalColumn: "Id", 
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_AspNetUsers_Clientes_ClienteId",
            table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "AspNetUsers");
        }
    }
}
