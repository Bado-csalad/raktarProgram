using Microsoft.EntityFrameworkCore;
using raktarProgram.Data;

namespace raktarProgram.Repositories
{
    public class RaktarContext : DbContext
    {
        public RaktarContext(DbContextOptions<RaktarContext> options) : base(options)
        {
        }

        public DbSet<Params> Params { get; set; }
        public DbSet<EszkozHely> EszkozHely { get; set; }
        public DbSet<EszkozHelyTipus> EszkozHelyTipus { get; set; }
        public DbSet<Eszkoz> Eszkoz { get; set; }
        public DbSet<Felhasznalo> Felhasznalo { get; set; }
        public DbSet<Hely> Hely { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EszkozHely>().ToTable("EszkozHelyek");
            modelBuilder.Entity<EszkozHelyTipus>().ToTable("EszkozHelyTipusok");
            modelBuilder.Entity<Eszkoz>().ToTable("Eszkozok");
            modelBuilder.Entity<Felhasznalo>().ToTable("Felhasznalok");
            modelBuilder.Entity<Hely>().ToTable("Helyek");
            modelBuilder.Entity<Params>().ToTable("Params");
        }
    }
}