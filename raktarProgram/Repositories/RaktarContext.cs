using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using raktarProgram.Data;
using System.Data.Common;
using System.Linq;
using System;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace raktarProgram.Repositories
{
    public class RaktarContext : IdentityDbContext
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
        public DbSet<EszkozTipus> EszkozTipus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<EszkozHely>().ToTable("EszkozHelyek");
            modelBuilder.Entity<EszkozHelyTipus>().ToTable("EszkozHelyTipusok");
            modelBuilder.Entity<Eszkoz>().ToTable("Eszkozok");
            modelBuilder.Entity<Felhasznalo>().ToTable("Felhasznalok");
            modelBuilder.Entity<Hely>().ToTable("Helyek");
            modelBuilder.Entity<Params>().ToTable("Params");
            modelBuilder.Entity<EszkozTipus>().ToTable("EszkozTipusok");
        }
    }

}
