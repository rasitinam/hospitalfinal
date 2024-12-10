using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace hospitalfinal.Models;

public partial class HospitalContext : DbContext
{
    public HospitalContext()
    {
    }

    public HospitalContext(DbContextOptions<HospitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branslar> Branslars { get; set; }

    public virtual DbSet<Doktorlar> Doktorlars { get; set; }

    public virtual DbSet<Hastalar> Hastalars { get; set; }

    public virtual DbSet<RandevuMusait> RandevuMusaits { get; set; }

    public virtual DbSet<Randevular> Randevulars { get; set; }

    public virtual DbSet<Sekreterler> Sekreterlers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Name=HospitalConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branslar>(entity =>
        {
            entity.HasKey(e => e.BransId);

            entity.ToTable("Branslar");
        });

        modelBuilder.Entity<Doktorlar>(entity =>
        {
            entity.HasKey(e => e.DoktorId);

            entity.ToTable("Doktorlar");

            entity.HasIndex(e => e.DoktorTc, "IX_Doktorlar_DoktorTC").IsUnique();

            entity.HasIndex(e => e.Email, "IX_Doktorlar_Email").IsUnique();

            entity.Property(e => e.DoktorTc).HasColumnName("DoktorTC");

            entity.HasOne(d => d.Brans).WithMany(p => p.Doktorlars)
                .HasForeignKey(d => d.BransId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Hastalar>(entity =>
        {
            entity.HasKey(e => e.HastaId);

            entity.ToTable("Hastalar");

            entity.HasIndex(e => e.Email, "IX_Hastalar_Email").IsUnique();

            entity.HasIndex(e => e.Tc, "IX_Hastalar_Tc").IsUnique();

            entity.HasIndex(e => e.Telefon, "IX_Hastalar_Telefon").IsUnique();

            entity.Property(e => e.DogumTarihi).HasColumnType("DATE");
        });

        modelBuilder.Entity<RandevuMusait>(entity =>
        {
            entity.ToTable("RandevuMusait");

            entity.Property(e => e.Durum).HasDefaultValue(1);
            entity.Property(e => e.MusaitSaat).HasColumnType("TIME");
            entity.Property(e => e.MusaitTarih).HasColumnType("DATE");

            entity.HasOne(d => d.Doktor).WithMany(p => p.RandevuMusaits)
                .HasForeignKey(d => d.DoktorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Randevular>(entity =>
        {
            entity.HasKey(e => e.RandevuId);

            entity.ToTable("Randevular");

            entity.Property(e => e.RandevuSaati).HasColumnType("TIME");
            entity.Property(e => e.RandevuTarihi).HasColumnType("DATE");

            entity.HasOne(d => d.Doktor).WithMany(p => p.Randevulars)
                .HasForeignKey(d => d.DoktorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Hasta).WithMany(p => p.Randevulars)
                .HasForeignKey(d => d.HastaId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Sekreterler>(entity =>
        {
            entity.HasKey(e => e.SekreterId);

            entity.ToTable("Sekreterler");

            entity.HasIndex(e => e.Email, "IX_Sekreterler_Email").IsUnique();

            entity.HasIndex(e => e.SekreterTc, "IX_Sekreterler_SekreterTC").IsUnique();

            entity.Property(e => e.SekreterTc).HasColumnName("SekreterTC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
