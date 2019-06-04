using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Danrevi.API.Models
{
    public partial class DanreviDbContext : DbContext
    {
        public DanreviDbContext()
        {
        }

        public DanreviDbContext(DbContextOptions<DanreviDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brugere> Brugere { get; set; }
        public virtual DbSet<Deadline> Deadline { get; set; }
        public virtual DbSet<Kurser> Kurser { get; set; }
        public virtual DbSet<Møder> Møder { get; set; }
        public virtual DbSet<Nyheder> Nyheder { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=DanreviDb;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brugere>(entity =>
            {
                entity.HasKey(e => e.FirebaseUid);

                entity.Property(e => e.FirebaseUid)
                    .HasMaxLength(30)
                    .ValueGeneratedNever();

                entity.Property(e => e.DisplayName).HasMaxLength(250);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<Deadline>(entity =>
            {
                entity.Property(e => e.Beskrivelse).IsRequired();

                entity.Property(e => e.Dato).HasColumnType("date");

                entity.Property(e => e.Navn).IsRequired();
            });

            modelBuilder.Entity<Kurser>(entity =>
            {
                entity.Property(e => e.Arrangør).IsRequired();

                entity.Property(e => e.Beskrivelse).IsRequired();

                entity.Property(e => e.Lokation).IsRequired();

                entity.Property(e => e.Målgruppe).IsRequired();

                entity.Property(e => e.Navn).IsRequired();

                entity.Property(e => e.Slut).HasColumnType("datetime");

                entity.Property(e => e.Start).HasColumnType("datetime");
            });

            modelBuilder.Entity<Møder>(entity =>
            {
                entity.Property(e => e.Beskrivelse).IsRequired();

                entity.Property(e => e.Lokation).IsRequired();

                entity.Property(e => e.Navn).IsRequired();

                entity.Property(e => e.Slut).HasColumnType("datetime");

                entity.Property(e => e.Start).HasColumnType("datetime");
            });

            modelBuilder.Entity<Nyheder>(entity =>
            {
                entity.Property(e => e.Forfatter).IsRequired();

                entity.Property(e => e.Indhold).IsRequired();

                entity.Property(e => e.OprettelsesDato).HasColumnType("datetime");

                entity.Property(e => e.Titel).IsRequired();
            });
        }
    }
}
