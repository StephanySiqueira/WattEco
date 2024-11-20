using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WattEco.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO_WATT",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_USUARIO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMAIL_USUARIO = table.Column<string>(type: "varchar(255)", nullable: false),
                    SENHA_USUARIO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_WATT", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "MISSAO_WATT",
                columns: table => new
                {
                    ID_MISSAO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PONTUACAO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MISSAO_WATT", x => x.ID_MISSAO);
                    table.ForeignKey(
                        name: "FK_MISSAO_WATT_USUARIO_WATT_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO_WATT",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RECOMPENSA_WAITT",
                columns: table => new
                {
                    ID_RECOMPENSA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DESCRICAO_RECOMPENSA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PONTOS_NECESSARIOS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RECOMPENSA_WAITT", x => x.ID_RECOMPENSA);
                    table.ForeignKey(
                        name: "FK_RECOMPENSA_WAITT_USUARIO_WATT_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO_WATT",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RELATORIO_WATT",
                columns: table => new
                {
                    ID_RELATORIO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CONSUMO_KWH = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    EMISSAO_CO2 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    PERIODO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RELATORIO_WATT", x => x.ID_RELATORIO);
                    table.ForeignKey(
                        name: "FK_RELATORIO_WATT_USUARIO_WATT_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO_WATT",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HISTORICO_WATT",
                columns: table => new
                {
                    ID_HISTORICO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_MISSAO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_RECOMPENSA = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PONTOS = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    DATA_HISTORICO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HISTORICO_WATT", x => x.ID_HISTORICO);
                    table.ForeignKey(
                        name: "FK_HISTORICO_WATT_MISSAO_WATT_ID_MISSAO",
                        column: x => x.ID_MISSAO,
                        principalTable: "MISSAO_WATT",
                        principalColumn: "ID_MISSAO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HISTORICO_WATT_RECOMPENSA_WAITT_ID_RECOMPENSA",
                        column: x => x.ID_RECOMPENSA,
                        principalTable: "RECOMPENSA_WAITT",
                        principalColumn: "ID_RECOMPENSA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HISTORICO_WATT_USUARIO_WATT_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO_WATT",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HISTORICO_WATT_ID_MISSAO",
                table: "HISTORICO_WATT",
                column: "ID_MISSAO");

            migrationBuilder.CreateIndex(
                name: "IX_HISTORICO_WATT_ID_RECOMPENSA",
                table: "HISTORICO_WATT",
                column: "ID_RECOMPENSA");

            migrationBuilder.CreateIndex(
                name: "IX_HISTORICO_WATT_ID_USUARIO",
                table: "HISTORICO_WATT",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_MISSAO_WATT_ID_USUARIO",
                table: "MISSAO_WATT",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_RECOMPENSA_WAITT_ID_USUARIO",
                table: "RECOMPENSA_WAITT",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_RELATORIO_WATT_ID_USUARIO",
                table: "RELATORIO_WATT",
                column: "ID_USUARIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HISTORICO_WATT");

            migrationBuilder.DropTable(
                name: "RELATORIO_WATT");

            migrationBuilder.DropTable(
                name: "MISSAO_WATT");

            migrationBuilder.DropTable(
                name: "RECOMPENSA_WAITT");

            migrationBuilder.DropTable(
                name: "USUARIO_WATT");
        }
    }
}
