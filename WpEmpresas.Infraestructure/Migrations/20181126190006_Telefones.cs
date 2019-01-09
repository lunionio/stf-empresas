using Microsoft.EntityFrameworkCore.Migrations;

namespace WpEmpresas.Infraestructure.Migrations
{
    public partial class Telefones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Telefone_Empresas_EmpresaId",
                table: "Telefone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Telefone",
                table: "Telefone");

            migrationBuilder.RenameTable(
                name: "Telefone",
                newName: "Telefones");

            migrationBuilder.RenameIndex(
                name: "IX_Telefone_EmpresaId",
                table: "Telefones",
                newName: "IX_Telefones_EmpresaId");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Telefones",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Telefones",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Telefones",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Telefones",
                table: "Telefones",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Telefones_Empresas_EmpresaId",
                table: "Telefones",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Telefones_Empresas_EmpresaId",
                table: "Telefones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Telefones",
                table: "Telefones");

            migrationBuilder.RenameTable(
                name: "Telefones",
                newName: "Telefone");

            migrationBuilder.RenameIndex(
                name: "IX_Telefones_EmpresaId",
                table: "Telefone",
                newName: "IX_Telefone_EmpresaId");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Telefone",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Telefone",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Telefone",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Telefone",
                table: "Telefone",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Telefone_Empresas_EmpresaId",
                table: "Telefone",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
