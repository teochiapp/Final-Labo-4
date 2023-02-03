using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationLabo4.Migrations
{
    public partial class categoriaIdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "categoriaId",
                table: "Clubes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clubes_Categoria_categoriaId",
                table: "Clubes",
                column: "categoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Clubes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubes_Categoria_CategoriaId",
                table: "Clubes",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id");
        }
    }
}
