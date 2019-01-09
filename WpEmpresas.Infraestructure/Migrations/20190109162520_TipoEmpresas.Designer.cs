﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WpEmpresas.Infraestructure;

namespace WpEmpresas.Infraestructure.Migrations
{
    [DbContext(typeof(WpEmpresasContext))]
    [Migration("20190109162520_TipoEmpresas")]
    partial class TipoEmpresas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WpEmpresas.Entities.Contato", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<string>("ContatoInfo")
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<DateTime>("DateAlteracao");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("EmpresaId");

                    b.Property<int>("IdCliente");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Status");

                    b.Property<int>("TipoId");

                    b.Property<int>("UsuarioCriacao");

                    b.Property<int>("UsuarioEdicao");

                    b.HasKey("ID");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("TipoId");

                    b.ToTable("Contatos");
                });

            modelBuilder.Entity("WpEmpresas.Entities.Empresa", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<string>("CNAE_S")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<int>("CodigoExterno");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<DateTime>("DateAlteracao");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Email");

                    b.Property<int>("IdCliente");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("RazaoSocial");

                    b.Property<int>("Status");

                    b.Property<int>("TipoEmpresaId");

                    b.Property<int>("UsuarioCriacao");

                    b.Property<int>("UsuarioEdicao");

                    b.HasKey("ID");

                    b.HasIndex("TipoEmpresaId");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("WpEmpresas.Entities.Endereco", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("CEP")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Cidade")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<DateTime>("DateAlteracao");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("EmpresaId");

                    b.Property<string>("Estado")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("IdCliente");

                    b.Property<int>("IdUsuario");

                    b.Property<string>("Local")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("NumeroLocal");

                    b.Property<int>("Status");

                    b.Property<string>("Uf");

                    b.Property<int>("UsuarioCriacao");

                    b.Property<int>("UsuarioEdicao");

                    b.HasKey("ID");

                    b.HasIndex("EmpresaId")
                        .IsUnique();

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("WpEmpresas.Entities.Telefone", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<DateTime>("DateAlteracao");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("EmpresaId");

                    b.Property<int>("IdCliente");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Numero")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Status");

                    b.Property<int>("UsuarioCriacao");

                    b.Property<int>("UsuarioEdicao");

                    b.HasKey("ID");

                    b.HasIndex("EmpresaId")
                        .IsUnique();

                    b.ToTable("Telefones");
                });

            modelBuilder.Entity("WpEmpresas.Entities.Tipo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<DateTime>("DateAlteracao");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("IdCliente");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Status");

                    b.Property<int>("UsuarioCriacao");

                    b.Property<int>("UsuarioEdicao");

                    b.HasKey("ID");

                    b.ToTable("Tipos");
                });

            modelBuilder.Entity("WpEmpresas.Entities.TipoEmpresa", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<DateTime>("DateAlteracao");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("IdCliente");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Status");

                    b.Property<int>("UsuarioCriacao");

                    b.Property<int>("UsuarioEdicao");

                    b.HasKey("ID");

                    b.ToTable("TipoEmpresas");
                });

            modelBuilder.Entity("WpEmpresas.Entities.Contato", b =>
                {
                    b.HasOne("WpEmpresas.Entities.Empresa", "Empresa")
                        .WithMany("Contatos")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WpEmpresas.Entities.Tipo", "Tipo")
                        .WithMany()
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WpEmpresas.Entities.Empresa", b =>
                {
                    b.HasOne("WpEmpresas.Entities.TipoEmpresa", "TipoEmpresa")
                        .WithMany()
                        .HasForeignKey("TipoEmpresaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WpEmpresas.Entities.Endereco", b =>
                {
                    b.HasOne("WpEmpresas.Entities.Empresa", "Empresa")
                        .WithOne("Endereco")
                        .HasForeignKey("WpEmpresas.Entities.Endereco", "EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WpEmpresas.Entities.Telefone", b =>
                {
                    b.HasOne("WpEmpresas.Entities.Empresa", "Empresa")
                        .WithOne("Telefone")
                        .HasForeignKey("WpEmpresas.Entities.Telefone", "EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
