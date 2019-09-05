using Microsoft.EntityFrameworkCore.Migrations;

namespace TDSTecnologia.Site.Infrastructure.Migrations
{
    public partial class createschematds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tds");

            migrationBuilder.RenameTable(
                name: "tb03_permissao",
                schema: "tdstecnologia",
                newName: "tb03_permissao",
                newSchema: "tds");

            migrationBuilder.RenameTable(
                name: "tb02_usuario",
                schema: "tdstecnologia",
                newName: "tb02_usuario",
                newSchema: "tds");

            migrationBuilder.RenameTable(
                name: "tb01_curso",
                schema: "tdstecnologia",
                newName: "tb01_curso",
                newSchema: "tds");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "tdstecnologia",
                newName: "AspNetUserTokens",
                newSchema: "tds");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "tdstecnologia",
                newName: "AspNetUserRoles",
                newSchema: "tds");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "tdstecnologia",
                newName: "AspNetUserLogins",
                newSchema: "tds");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "tdstecnologia",
                newName: "AspNetUserClaims",
                newSchema: "tds");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "tdstecnologia",
                newName: "AspNetRoleClaims",
                newSchema: "tds");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tdstecnologia");

            migrationBuilder.RenameTable(
                name: "tb03_permissao",
                schema: "tds",
                newName: "tb03_permissao",
                newSchema: "tdstecnologia");

            migrationBuilder.RenameTable(
                name: "tb02_usuario",
                schema: "tds",
                newName: "tb02_usuario",
                newSchema: "tdstecnologia");

            migrationBuilder.RenameTable(
                name: "tb01_curso",
                schema: "tds",
                newName: "tb01_curso",
                newSchema: "tdstecnologia");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "tds",
                newName: "AspNetUserTokens",
                newSchema: "tdstecnologia");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "tds",
                newName: "AspNetUserRoles",
                newSchema: "tdstecnologia");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "tds",
                newName: "AspNetUserLogins",
                newSchema: "tdstecnologia");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "tds",
                newName: "AspNetUserClaims",
                newSchema: "tdstecnologia");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "tds",
                newName: "AspNetRoleClaims",
                newSchema: "tdstecnologia");
        }
    }
}
