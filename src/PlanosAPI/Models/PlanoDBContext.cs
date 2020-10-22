using Microsoft.EntityFrameworkCore;

namespace PlanosAPI.Models
{
    public class PlanoDBContext : DbContext
    {
        public PlanoDBContext(DbContextOptions<PlanoDBContext> options) : base(options)
        {
        }

        public DbSet<Plano> Planos { get; set; }
        public DbSet<TipoPlano> TiposPlanos { get; set; }
        public DbSet<Operadora> Operadoras { get; set; }
        public DbSet<DDD> DDDs { get; set; }
        public DbSet<PlanoDDD> PlanosDDDs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlanoDDD>()
                .HasKey(t => new { t.CodigoDDD, t.IdPlano });

            modelBuilder.Entity<PlanoDDD>()
                .HasOne(pt => pt.DDD)
                .WithMany(p => p.PlanoDDD)
                .HasForeignKey(pt => pt.CodigoDDD);

            modelBuilder.Entity<PlanoDDD>()
                .HasOne(pt => pt.Plano)
                .WithMany(t => t.PlanoDDD)
                .HasForeignKey(pt => pt.IdPlano);

        }
    }
}
