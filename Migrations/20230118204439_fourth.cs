using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationLabo4.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaClubes_Categoria_idCategoriaId",
                table: "CategoriaClubes");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaClubes_Clubes_ClubesId",
                table: "CategoriaClubes");

            migrationBuilder.RenameColumn(
                name: "idCategoriaId",
                table: "CategoriaClubes",
                newName: "ClubId");

            migrationBuilder.RenameColumn(
                name: "ClubesId",
                table: "CategoriaClubes",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriaClubes_idCategoriaId",
                table: "CategoriaClubes",
                newName: "IX_CategoriaClubes_ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaClubes_Categoria_CategoriaId",
                table: "CategoriaClubes",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaClubes_Clubes_ClubId",
                table: "CategoriaClubes",
                column: "ClubId",
                principalTable: "Clubes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaClubes_Categoria_CategoriaId",
                table: "CategoriaClubes");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaClubes_Clubes_ClubId",
                table: "CategoriaClubes");

            migrationBuilder.RenameColumn(
                name: "ClubId",
                table: "CategoriaClubes",
                newName: "idCategoriaId");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "CategoriaClubes",
                newName: "ClubesId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriaClubes_ClubId",
                table: "CategoriaClubes",
                newName: "IX_CategoriaClubes_idCategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaClubes_Categoria_idCategoriaId",
                table: "CategoriaClubes",
                column: "idCategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaClubes_Clubes_ClubesId",
                table: "CategoriaClubes",
                column: "ClubesId",
                principalTable: "Clubes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
