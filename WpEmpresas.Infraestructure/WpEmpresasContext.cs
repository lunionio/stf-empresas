using Microsoft.EntityFrameworkCore;
using WpEmpresas.Entities;

namespace WpEmpresas.Infraestructure
{
    public class WpEmpresasContext : DbContext
    {
        public DbSet<Empresa> Empresas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=34.226.175.244;Initial Catalog=WebPixEmpresas;Persist Security Info=True;User ID=sa;Password=StaffPro@123;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empresa>(ep =>
            {
                ep.Property(e => e.Nome).HasColumnType("varchar(200)");
                ep.Property(e => e.Descricao).HasColumnType("varchar(200)");
            });
        }
    }
}
