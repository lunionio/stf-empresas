using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WpEmpresas.Infraestructure.Migrations
{
    public partial class TipoEmpresas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uf",
                table: "Enderecos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CodigoExterno",
                table: "Empresas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoEmpresaId",
                table: "Empresas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TipoEmpresas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DateAlteracao = table.Column<DateTime>(nullable: false),
                    UsuarioCriacao = table.Column<int>(nullable: false),
                    UsuarioEdicao = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IdCliente = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEmpresas", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_TipoEmpresaId",
                table: "Empresas",
                column: "TipoEmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_TipoEmpresas_TipoEmpresaId",
                table: "Empresas",
                column: "TipoEmpresaId",
                principalTable: "TipoEmpresas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_TipoEmpresas_TipoEmpresaId",
                table: "Empresas");

            migrationBuilder.DropTable(
                name: "TipoEmpresas");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_TipoEmpresaId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Uf",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "CodigoExterno",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "TipoEmpresaId",
                table: "Empresas");
        }
    }
}
