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
        public virtual DbSet<BrugerKurser> BrugerKurser { get; set; }
        public virtual DbSet<Deadline> Deadline { get; set; }
        public virtual DbSet<Kurser> Kurser { get; set; }
        public virtual DbSet<Møder> Møder { get; set; }
        public virtual DbSet<MøderBruger> MøderBruger { get; set; }
        public virtual DbSet<Nyheder> Nyheder { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
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

            modelBuilder.Entity<BrugerKurser>(entity =>
            {
                entity.HasKey(e => new { e.KursusId, e.Uid });

                entity.ToTable("Bruger_Kurser");

                entity.Property(e => e.Uid).HasMaxLength(30);

                entity.HasOne(d => d.Kursus)
                    .WithMany(p => p.BrugerKurser)
                    .HasForeignKey(d => d.KursusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bruger_Kurser_Kurser");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.BrugerKurser)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bruger_Kurser_Brugere");
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

            modelBuilder.Entity<MøderBruger>(entity =>
            {
                entity.HasKey(e => new { e.MødeId, e.Uid });

                entity.ToTable("Møder_Bruger");

                entity.Property(e => e.Uid).HasMaxLength(30);

                entity.HasOne(d => d.Møde)
                    .WithMany(p => p.MøderBruger)
                    .HasForeignKey(d => d.MødeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Møder_Bruger_Møder");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.MøderBruger)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Møder_Bruger_Brugere");
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
