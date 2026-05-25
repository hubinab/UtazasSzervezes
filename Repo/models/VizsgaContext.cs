using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Repo.models;

public partial class VizsgaContext : DbContext
{

    public int? UtiBevetel(int napidij, int uticelId)
    {
        var uticel = Uticels.Find(uticelId);
        if (uticel == null) return null;
        return uticel.Idotartam * napidij;
    }

    public VizsgaContext()
    {
    }

    public VizsgaContext(DbContextOptions<VizsgaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Idegenvezeto> Idegenvezetos { get; set; }

    public virtual DbSet<Megrendelo> Megrendelos { get; set; }

    public virtual DbSet<Utaza> Utazas { get; set; }

    public virtual DbSet<Uticel> Uticels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=127.0.0.1;database=vizsga;uid=root;pwd=jelszo", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.3.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Idegenvezeto>(entity =>
        {
            entity.HasKey(e => e.IdegenvezetoId).HasName("PRIMARY");

            entity
                .ToTable("idegenvezeto")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_hungarian_ci");

            entity.Property(e => e.IdegenvezetoId).HasColumnName("idegenvezeto_id");
            entity.Property(e => e.Email)
                .HasMaxLength(49)
                .HasColumnName("email")
                .UseCollation("utf8mb3_general_ci");
            entity.Property(e => e.Napidij).HasColumnName("napidij");
            entity.Property(e => e.Nev)
                .HasMaxLength(25)
                .HasColumnName("nev")
                .UseCollation("utf8mb3_general_ci");
            entity.Property(e => e.Telefon)
                .HasMaxLength(21)
                .HasColumnName("telefon")
                .UseCollation("utf8mb3_general_ci");
        });

        modelBuilder.Entity<Megrendelo>(entity =>
        {
            entity.HasKey(e => e.MegrendeloId).HasName("PRIMARY");

            entity
                .ToTable("megrendelo")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_hungarian_ci");

            entity.Property(e => e.MegrendeloId).HasColumnName("megrendelo_id");
            entity.Property(e => e.Email)
                .HasMaxLength(51)
                .HasColumnName("email")
                .UseCollation("utf8mb3_general_ci");
            entity.Property(e => e.Nev)
                .HasMaxLength(27)
                .HasColumnName("nev")
                .UseCollation("utf8mb3_general_ci");
            entity.Property(e => e.Telefon)
                .HasMaxLength(21)
                .HasColumnName("telefon")
                .UseCollation("utf8mb3_general_ci");
        });

        modelBuilder.Entity<Utaza>(entity =>
        {
            entity.HasKey(e => e.UtazasId).HasName("PRIMARY");

            entity
                .ToTable("utazas")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_hungarian_ci");

            entity.HasIndex(e => e.MegrendeloId, "megrendelo_id");

            entity.HasIndex(e => e.UticelId, "uticel_id");

            entity.Property(e => e.UtazasId).HasColumnName("utazas_id");
            entity.Property(e => e.Datum).HasColumnName("datum");
            entity.Property(e => e.MegrendeloId).HasColumnName("megrendelo_id");
            entity.Property(e => e.Utasszam).HasColumnName("utasszam");
            entity.Property(e => e.UticelId).HasColumnName("uticel_id");

            entity.HasOne(d => d.Megrendelo).WithMany(p => p.Utazas)
                .HasForeignKey(d => d.MegrendeloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("utazas_ibfk_1");

            entity.HasOne(d => d.Uticel).WithMany(p => p.Utazas)
                .HasForeignKey(d => d.UticelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("utazas_ibfk_2");
        });

        modelBuilder.Entity<Uticel>(entity =>
        {
            entity.HasKey(e => e.UticelId).HasName("PRIMARY");

            entity
                .ToTable("uticel")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_hungarian_ci");

            entity.HasIndex(e => e.IdegenvezetoId, "idegenvezeto_id");

            entity.Property(e => e.UticelId).HasColumnName("uticel_id");
            entity.Property(e => e.IdegenvezetoId).HasColumnName("idegenvezeto_id");
            entity.Property(e => e.Idotartam).HasColumnName("idotartam");
            entity.Property(e => e.Megnevezes)
                .HasMaxLength(45)
                .HasColumnName("megnevezes")
                .UseCollation("utf8mb3_general_ci");

            entity.HasOne(d => d.Idegenvezeto).WithMany(p => p.Uticels)
                .HasForeignKey(d => d.IdegenvezetoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("uticel_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
