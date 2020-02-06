using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppOscar.Persistence.AppOscar.Persistence.SqlServer
{
    public partial class InitialCreateSqlServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filmes",
                columns: table => new
                {
                    IdFilme = table.Column<Guid>(nullable: false),
                    NomeFilme = table.Column<string>(nullable: false),
                    FilmePhotoUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmes", x => x.IdFilme);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id_user = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeUsuario = table.Column<string>(nullable: false),
                    emailUsuario = table.Column<string>(nullable: false),
                    senhaUsuario = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id_user);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    IdCategoria = table.Column<Guid>(nullable: false),
                    NomeCategoria = table.Column<string>(nullable: false),
                    PontosCategoria = table.Column<int>(nullable: false),
                    FilmeVencedorId = table.Column<Guid>(nullable: true),
                    CategoriaPhotoUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.IdCategoria);
                    table.ForeignKey(
                        name: "FK_Categorias_Filmes_FilmeVencedorId",
                        column: x => x.FilmeVencedorId,
                        principalTable: "Filmes",
                        principalColumn: "IdFilme",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Participacoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCategoria = table.Column<Guid>(nullable: false),
                    IdFilme = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participacoes_Categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Participacoes_Filmes_IdFilme",
                        column: x => x.IdFilme,
                        principalTable: "Filmes",
                        principalColumn: "IdFilme",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Votos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DthCriacao = table.Column<DateTimeOffset>(nullable: false),
                    IdParticipacao = table.Column<int>(nullable: false),
                    IdUsuario = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votos_Participacoes_IdParticipacao",
                        column: x => x.IdParticipacao,
                        principalTable: "Participacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_FilmeVencedorId",
                table: "Categorias",
                column: "FilmeVencedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Participacoes_IdCategoria",
                table: "Participacoes",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Participacoes_IdFilme",
                table: "Participacoes",
                column: "IdFilme");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_IdParticipacao",
                table: "Votos",
                column: "IdParticipacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Votos");

            migrationBuilder.DropTable(
                name: "Participacoes");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Filmes");
        }
    }
}
