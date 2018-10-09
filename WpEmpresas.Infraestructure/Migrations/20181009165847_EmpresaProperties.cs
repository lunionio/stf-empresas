using Microsoft.EntityFrameworkCore.Migrations;

namespace WpEmpresas.Infraestructure.Migrations
{
    public partial class EmpresaProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_Empresas_EmpresaID",
                table: "Contatos");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Contatos");

            migrationBuilder.RenameColumn(
                name: "EmpresaID",
                table: "Contatos",
                newName: "EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_Contatos_EmpresaID",
                table: "Contatos",
                newName: "IX_Contatos_EmpresaId");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Contatos",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_Empresas_EmpresaId",
                table: "Contatos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_Empresas_EmpresaId",
                table: "Contatos");

            migrationBuilder.RenameColumn(
                name: "EmpresaId",
                table: "Contatos",
                newName: "EmpresaID");

            migrationBuilder.RenameIndex(
                name: "IX_Contatos_EmpresaId",
                table: "Contatos",
                newName: "IX_Contatos_EmpresaID");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaID",
                table: "Contatos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Contatos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_Empresas_EmpresaID",
                table: "Contatos",
                column: "EmpresaID",
                principalTable: "Empresas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
