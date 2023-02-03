﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationLabo4.Migrations
{
    public partial class categoriaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubes_Categoria_CategoriaId",
                table: "Clubes");

            migrationBuilder.DropIndex(
                name: "IX_Clubes_CategoriaId",
                table: "Clubes");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Clubes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClubesId",
                table: "Categoria",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_ClubesId",
                table: "Categoria",
                column: "ClubesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_Clubes_ClubesId",
                table: "Categoria",
                column: "ClubesId",
                principalTable: "Clubes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_Clubes_ClubesId",
                table: "Categoria");

            migrationBuilder.DropIndex(
                name: "IX_Categoria_ClubesId",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "ClubesId",
                table: "Categoria");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Clubes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Clubes_CategoriaId",
                table: "Clubes",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubes_Categoria_CategoriaId",
                table: "Clubes",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id");
        }
    }
}