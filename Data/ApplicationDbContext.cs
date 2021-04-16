using DeputiTigaKemenpora.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora.Data
{
   public partial class ApplicationDbContext : IdentityDbContext
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

      public virtual DbSet<KabupatenKota> KabupatenKota { get; set; }
      public virtual DbSet<Kegiatan> Kegiatan { get; set; }
      public virtual DbSet<PenanggungJawab> PenanggungJawab { get; set; }
      public virtual DbSet<Provinsi> Provinsi { get; set; }
      public virtual DbSet<SumberDana> SumberDana { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<KabupatenKota>(entity =>
         {
            entity.HasKey(e => e.Kode)
               .HasName("PRIMARY");

            entity.ToTable("kabupaten_kota");

            entity.HasIndex(e => e.KodeProvinsi)
               .HasName("FK_kabupaten_kota_provinsi");

            entity.Property(e => e.Kode).HasColumnType("int(11)");

            entity.Property(e => e.KodeProvinsi).HasColumnType("tinyint(4)");

            entity.Property(e => e.Lat).HasColumnType("decimal(9,6)");

            entity.Property(e => e.Long).HasColumnType("decimal(9,6)");

            entity.Property(e => e.Nama)
               .IsRequired()
               .HasColumnType("tinytext")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");
         });

         modelBuilder.Entity<Kegiatan>(entity =>
         {
            entity.ToTable("kegiatan");

            entity.HasIndex(e => e.KabupatenKota)
               .HasName("FK_kegiatan_kabupaten_kota");

            entity.HasIndex(e => e.PenanggungJawab)
               .HasName("FK_kegiatan_penanggung_jawab");

            entity.HasIndex(e => e.SumberDana)
               .HasName("FK_kegiatan_sumber_dana");

            entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

            entity.Property(e => e.AdaKendala).HasColumnType("tinyint(4)");

            entity.Property(e => e.FilePendukung1)
               .IsRequired()
               .HasColumnType("tinytext")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");

            entity.Property(e => e.FilePendukung2)
               .IsRequired()
               .HasColumnType("tinytext")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");

            entity.Property(e => e.FilePendukung3)
               .IsRequired()
               .HasColumnType("tinytext")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");

            entity.Property(e => e.FotoKegiatan1)
               .IsRequired()
               .HasColumnType("tinytext")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");

            entity.Property(e => e.FotoKegiatan2)
               .IsRequired()
               .HasColumnType("tinytext")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");

            entity.Property(e => e.FotoKegiatan3)
               .IsRequired()
               .HasColumnType("tinytext")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");

            entity.Property(e => e.FotoKegiatan4)
               .IsRequired()
               .HasColumnType("tinytext")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");

            entity.Property(e => e.FotoKegiatan5)
               .IsRequired()
               .HasColumnType("tinytext")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");

            entity.Property(e => e.JumlahPeserta).HasColumnType("int(10) unsigned");

            entity.Property(e => e.KabupatenKota).HasColumnType("int(11)");

            entity.Property(e => e.Kendala)
               .HasColumnType("tinytext")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");

            entity.Property(e => e.LinkBerita1)
               .IsRequired()
               .HasColumnType("text")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");

            entity.Property(e => e.LinkBerita2)
               .IsRequired()
               .HasColumnType("text")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");

            entity.Property(e => e.LinkBerita3)
               .IsRequired()
               .HasColumnType("text")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");

            entity.Property(e => e.Nama)
               .IsRequired()
               .HasColumnType("tinytext")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");

            entity.Property(e => e.NamaPejabatPembuka)
               .IsRequired()
               .HasColumnType("tinytext")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");

            entity.Property(e => e.PenanggungJawab).HasColumnType("smallint(5) unsigned");

            entity.Property(e => e.SumberDana).HasColumnType("tinyint(3) unsigned");

            entity.Property(e => e.TanggalMulai).HasColumnType("datetime");

            entity.Property(e => e.TanggalSelesai).HasColumnType("datetime");

            entity.Property(e => e.Tempat)
               .IsRequired()
               .HasColumnType("tinytext")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");

            entity.HasOne(d => d.KabupatenKotaNavigation)
               .WithMany(p => p.Kegiatan)
               .HasForeignKey(d => d.KabupatenKota)
               .HasConstraintName("FK_kegiatan_kabupaten_kota");

            entity.HasOne(d => d.PenanggungJawabNavigation)
               .WithMany(p => p.Kegiatan)
               .HasForeignKey(d => d.PenanggungJawab)
               .HasConstraintName("FK_kegiatan_penanggung_jawab");

            entity.HasOne(d => d.SumberDanaNavigation)
               .WithMany(p => p.Kegiatan)
               .HasForeignKey(d => d.SumberDana)
               .HasConstraintName("FK_kegiatan_sumber_dana");
         });

         modelBuilder.Entity<PenanggungJawab>(entity =>
         {
            entity.ToTable("penanggung_jawab");

            entity.Property(e => e.Id).HasColumnType("smallint(5) unsigned");

            entity.Property(e => e.Nama)
               .IsRequired()
               .HasColumnType("tinytext")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");
         });

         modelBuilder.Entity<Provinsi>(entity =>
         {
            entity.HasKey(e => e.Kode)
               .HasName("PRIMARY");

            entity.ToTable("provinsi");

            entity.Property(e => e.Kode)
               .HasColumnType("tinyint(4)")
               .ValueGeneratedNever();

            entity.Property(e => e.Lat).HasColumnType("decimal(9,6)");

            entity.Property(e => e.Long).HasColumnType("decimal(9,6)");

            entity.Property(e => e.Nama)
               .IsRequired()
               .HasColumnType("tinytext")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");
         });

         modelBuilder.Entity<SumberDana>(entity =>
         {
            entity.ToTable("sumber_dana");

            entity.Property(e => e.Id)
               .HasColumnType("tinyint(3) unsigned")
               .ValueGeneratedOnAdd();

            entity.Property(e => e.Nama)
               .IsRequired()
               .HasColumnType("tinytext")
               .HasDefaultValueSql("''")
               .HasCharSet("utf8")
               .HasCollation("utf8_general_ci");
         });

         base.OnModelCreating(modelBuilder);
         OnModelCreatingPartial(modelBuilder);
      }

      partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
   }
}
