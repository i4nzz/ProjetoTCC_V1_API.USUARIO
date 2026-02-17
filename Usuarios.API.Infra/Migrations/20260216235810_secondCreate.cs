using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Usuarios.API.Infra.Migrations
{
    /// <inheritdoc />
    public partial class secondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "usuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuario",
                table: "usuario",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "categoria_financeira",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria_financeira", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "filho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_filho_usuario_Id",
                        column: x => x.Id,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pai",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pai", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pai_usuario_Id",
                        column: x => x.Id,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mesada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilhoId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mesada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mesada_filho_FilhoId",
                        column: x => x.FilhoId,
                        principalTable: "filho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "recompensa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilhoId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PontosNecessarios = table.Column<int>(type: "int", nullable: false),
                    Ativa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recompensa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recompensa_filho_FilhoId",
                        column: x => x.FilhoId,
                        principalTable: "filho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tarefa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilhoId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Pontos = table.Column<int>(type: "int", nullable: false),
                    Prazo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tarefa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tarefa_filho_FilhoId",
                        column: x => x.FilhoId,
                        principalTable: "filho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pais_filhos",
                columns: table => new
                {
                    PaiId = table.Column<int>(type: "int", nullable: false),
                    FilhoId = table.Column<int>(type: "int", nullable: false),
                    DataVinculo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pais_filhos", x => new { x.PaiId, x.FilhoId });
                    table.ForeignKey(
                        name: "FK_pais_filhos_filho_FilhoId",
                        column: x => x.FilhoId,
                        principalTable: "filho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pais_filhos_pai_PaiId",
                        column: x => x.PaiId,
                        principalTable: "pai",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "registro_financeiro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilhoId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    MesadaId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registro_financeiro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_registro_financeiro_categoria_financeira_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "categoria_financeira",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_registro_financeiro_filho_FilhoId",
                        column: x => x.FilhoId,
                        principalTable: "filho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_registro_financeiro_mesada_MesadaId",
                        column: x => x.MesadaId,
                        principalTable: "mesada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "recompensa_resgatada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilhoId = table.Column<int>(type: "int", nullable: false),
                    RecompensaId = table.Column<int>(type: "int", nullable: false),
                    DataResgate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recompensa_resgatada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recompensa_resgatada_filho_FilhoId",
                        column: x => x.FilhoId,
                        principalTable: "filho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_recompensa_resgatada_recompensa_RecompensaId",
                        column: x => x.RecompensaId,
                        principalTable: "recompensa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comprovacao_tarefa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TarefaId = table.Column<int>(type: "int", nullable: false),
                    UrlFoto = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Validada = table.Column<bool>(type: "bit", nullable: false),
                    DataValidacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comprovacao_tarefa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comprovacao_tarefa_tarefa_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "tarefa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pontuacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilhoId = table.Column<int>(type: "int", nullable: false),
                    TarefaId = table.Column<int>(type: "int", nullable: false),
                    Pontos = table.Column<int>(type: "int", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pontuacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pontuacao_filho_FilhoId",
                        column: x => x.FilhoId,
                        principalTable: "filho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pontuacao_tarefa_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "tarefa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comprovacao_tarefa_TarefaId",
                table: "comprovacao_tarefa",
                column: "TarefaId");

            migrationBuilder.CreateIndex(
                name: "IX_mesada_FilhoId",
                table: "mesada",
                column: "FilhoId");

            migrationBuilder.CreateIndex(
                name: "IX_pais_filhos_FilhoId",
                table: "pais_filhos",
                column: "FilhoId");

            migrationBuilder.CreateIndex(
                name: "IX_pontuacao_FilhoId",
                table: "pontuacao",
                column: "FilhoId");

            migrationBuilder.CreateIndex(
                name: "IX_pontuacao_TarefaId",
                table: "pontuacao",
                column: "TarefaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_recompensa_FilhoId",
                table: "recompensa",
                column: "FilhoId");

            migrationBuilder.CreateIndex(
                name: "IX_recompensa_resgatada_FilhoId",
                table: "recompensa_resgatada",
                column: "FilhoId");

            migrationBuilder.CreateIndex(
                name: "IX_recompensa_resgatada_RecompensaId",
                table: "recompensa_resgatada",
                column: "RecompensaId");

            migrationBuilder.CreateIndex(
                name: "IX_registro_financeiro_CategoriaId",
                table: "registro_financeiro",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_registro_financeiro_FilhoId",
                table: "registro_financeiro",
                column: "FilhoId");

            migrationBuilder.CreateIndex(
                name: "IX_registro_financeiro_MesadaId",
                table: "registro_financeiro",
                column: "MesadaId");

            migrationBuilder.CreateIndex(
                name: "IX_tarefa_FilhoId",
                table: "tarefa",
                column: "FilhoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comprovacao_tarefa");

            migrationBuilder.DropTable(
                name: "pais_filhos");

            migrationBuilder.DropTable(
                name: "pontuacao");

            migrationBuilder.DropTable(
                name: "recompensa_resgatada");

            migrationBuilder.DropTable(
                name: "registro_financeiro");

            migrationBuilder.DropTable(
                name: "pai");

            migrationBuilder.DropTable(
                name: "tarefa");

            migrationBuilder.DropTable(
                name: "recompensa");

            migrationBuilder.DropTable(
                name: "categoria_financeira");

            migrationBuilder.DropTable(
                name: "mesada");

            migrationBuilder.DropTable(
                name: "filho");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuario",
                table: "usuario");

            migrationBuilder.RenameTable(
                name: "usuario",
                newName: "Usuarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");
        }
    }
}
