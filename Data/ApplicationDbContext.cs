using System;
using System.Collections.Generic;
using System.Text;
using DeputiTigaKemenpora.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public virtual DbSet<Kegiatan> Kegiatan { get; set; }
        public virtual DbSet<PenanggungJawab> PenanggungJawab { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Kegiatan>(entity =>
            {
                entity.ToTable("kegiatan");

                entity.HasIndex(e => e.PenanggungJawab)
                    .HasName("FK_kegiatan_penanggung_jawab");

                entity.Property(e => e.AdaKendala)
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.FilePendukung1)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.FilePendukung2)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.FilePendukung3)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.FotoKegiatan1)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.FotoKegiatan2)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.FotoKegiatan3)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.FotoKegiatan4)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.FotoKegiatan5)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.JumlahPeserta)
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Kendala)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkBerita1)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkBerita2)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkBerita3)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nama)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NamaPejabatPembuka)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.TanggalMulai).HasColumnType("datetime");

                entity.Property(e => e.TanggalSelesai).HasColumnType("datetime");

                entity.Property(e => e.Tempat)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.PenanggungJawabNavigation)
                    .WithMany(p => p.Kegiatan)
                    .HasForeignKey(d => d.PenanggungJawab)
                    .HasConstraintName("FK_kegiatan_penanggung_jawab");
            });

            modelBuilder.Entity<PenanggungJawab>(entity =>
            {
                entity.ToTable("penanggung_jawab");

                entity.Property(e => e.Nama)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });
        }
    }
}