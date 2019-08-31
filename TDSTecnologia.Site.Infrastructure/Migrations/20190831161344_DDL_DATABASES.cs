using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TDSTecnologia.Site.Infrastructure.Migrations
{
    public partial class DDL_DATABASES : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tdstecnologia");

            migrationBuilder.CreateTable(
                name: "tb01_curso",
                schema: "tdstecnologia",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nome = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    descricao = table.Column<string>(nullable: true),
                    quantidade_aula = table.Column<int>(nullable: false),
                    data_inicio = table.Column<DateTime>(nullable: false),
                    banner = table.Column<byte[]>(nullable: true),
                    turno = table.Column<string>(nullable: false),
                    modalidade = table.Column<string>(nullable: false),
                    nivel = table.Column<string>(nullable: false),
                    vagas = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb01_curso", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb02_usuario",
                schema: "tdstecnologia",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    nome = table.Column<string>(maxLength: 100, nullable: false),
                    cpf = table.Column<string>(nullable: false),
                    telefone = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb02_usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb03_permissao",
                schema: "tdstecnologia",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    descricao = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb03_permissao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "tdstecnologia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_tb02_usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "tdstecnologia",
                        principalTable: "tb02_usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "tdstecnologia",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_tb02_usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "tdstecnologia",
                        principalTable: "tb02_usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "tdstecnologia",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_tb02_usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "tdstecnologia",
                        principalTable: "tb02_usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "tdstecnologia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_tb03_permissao_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "tdstecnologia",
                        principalTable: "tb03_permissao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "tdstecnologia",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_tb03_permissao_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "tdstecnologia",
                        principalTable: "tb03_permissao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_tb02_usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "tdstecnologia",
                        principalTable: "tb02_usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "tdstecnologia",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "tdstecnologia",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "tdstecnologia",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "tdstecnologia",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tb02_usuario_cpf",
                schema: "tdstecnologia",
                table: "tb02_usuario",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "tdstecnologia",
                table: "tb02_usuario",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "tdstecnologia",
                table: "tb02_usuario",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "tdstecnologia",
                table: "tb03_permissao",
                column: "NormalizedName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "tdstecnologia");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "tdstecnologia");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "tdstecnologia");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "tdstecnologia");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "tdstecnologia");

            migrationBuilder.DropTable(
                name: "tb01_curso",
                schema: "tdstecnologia");

            migrationBuilder.DropTable(
                name: "tb03_permissao",
                schema: "tdstecnologia");

            migrationBuilder.DropTable(
                name: "tb02_usuario",
                schema: "tdstecnologia");
        }
    }
}
