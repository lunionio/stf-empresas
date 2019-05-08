using Microsoft.EntityFrameworkCore;
using WpEmpresas.Entities;

namespace WpEmpresas.Infraestructure
{
    public class WpEmpresasContext : DbContext
    {
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<TipoEmpresa> TipoEmpresas { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Responsavel> Responsavel { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<EmpresaXEspecialidade> EmpresasXEspecialidades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(@"Server=TSERVICES\SQLEXPRESS;Database=WebPixEmpresas;Trusted_Connection=True;Integrated Security = True;");
            optionsBuilder.UseSqlServer(@"Data Source=187.84.232.164;Initial Catalog=WebPixEmpresas;Persist Security Info=True;User ID=sa;Password=StaffPro@123;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empresa>(ep =>
            {
                ep.Property(e => e.Nome).HasColumnType("varchar(200)");
                ep.Property(e => e.Descricao).HasColumnType("varchar(200)");
                ep.Property(e => e.CNAE_S).HasColumnType("varchar(100)");
                ep.Property(e => e.CNPJ).HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Telefone>(tel =>
            {
                tel.Property(t => t.Nome).HasColumnType("varchar(200)");
                tel.Property(t => t.Descricao).HasColumnType("varchar(200)");
                tel.Property(t => t.Numero).HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<Tipo>(tp => 
            {
                tp.Property(t => t.Descricao).HasColumnType("varchar(200)");
                tp.Property(t => t.Nome).HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<Contato>(ct => 
            {
                ct.Property(c => c.Descricao).HasColumnType("varchar(200)");
                ct.Property(c => c.ContatoInfo).HasColumnType("varchar(150)");
                ct.Property(c => c.Nome).HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<Endereco>(ed => 
            {
                ed.Property(e => e.Nome).HasColumnType("varchar(50)");
                ed.Property(e => e.Descricao).HasColumnType("varchar(200)");
                ed.Property(e => e.CEP).HasColumnType("varchar(20)");
                ed.Property(e => e.Estado).HasColumnType("varchar(20)");
                ed.Property(e => e.Cidade).HasColumnType("varchar(20)");
                ed.Property(e => e.Bairro).HasColumnType("varchar(20)");
                ed.Property(e => e.Local).HasColumnType("varchar(50)");
                ed.Property(e => e.Complemento).HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<TipoEmpresa>(te =>
            {
                te.Property(x => x.Descricao).HasColumnType("varchar(200)");
                te.Property(x => x.Nome).HasColumnType("varchar(200)");
            });
        }
    }
}