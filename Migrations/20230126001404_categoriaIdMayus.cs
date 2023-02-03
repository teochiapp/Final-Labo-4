using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationLabo4.Migrations
{
    public partial class categoriaIdMayus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubes_Categoria_categoriaId",
                table: "Clubes");

            migrationBuilder.RenameColumn(
                name: "categoriaId",
                table: "Clubes",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Clubes_categoriaId",
                table: "Clubes",
                newName: "IX_Clubes_CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubes_Categoria_CategoriaId",
                table: "Clubes",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubes_Categoria_CategoriaId",
                table: "Clubes");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Clubes",
                newName: "categoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Clubes_CategoriaId",
                table: "Clubes",
                newName: "IX_Clubes_categoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubes_Categoria_categoriaId",
                table: "Clubes",
                column: "categoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
