using System;
using System.Collections.Generic;
using BPAMatchineTracking.Models;
using Microsoft.EntityFrameworkCore;

namespace BPAMatchineTracking.DemoModels;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblMcDamageCause> TblMcDamageCause { get; set; }
    public virtual DbSet<TblMcIdleCause> TblMcIdleCause { get; set; }
    public virtual DbSet<TblMcUmCause> TblMcUmCause { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=103.9.134.216;Database=COTTONCLUB;User Id=sa;Password=TKL@007#;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblMcDamageCause>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TBL_MC_DAMAGE_CAUSE");

            entity.Property(e => e.CauseName)
                .HasMaxLength(50)
                .HasColumnName("CAUSE_NAME");
            entity.Property(e => e.Dcid).HasColumnName("DCID");
            entity.Property(e => e.Remarks)
                .HasMaxLength(50)
                .HasColumnName("REMARKS");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<TblMcIdleCause>(entity =>
        {
            entity.HasKey(e => e.Icid).HasName("PK_TBL_IDLE_CAUSE_INFO");

            entity.ToTable("TBL_MC_IDLE_CAUSE");

            entity.Property(e => e.Icid).HasColumnName("ICID");
            entity.Property(e => e.CauseName)
                .HasMaxLength(50)
                .HasColumnName("CAUSE_NAME");
            entity.Property(e => e.Remarks)
                .HasMaxLength(50)
                .HasColumnName("REMARKS");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<TblMcUmCause>(entity =>
        {
            entity.HasKey(e => e.Umcid).HasName("PK_TBL_MC_UNDER_MAINTENANCE_CAUSE");

            entity.ToTable("TBL_MC_UM_CAUSE");

            entity.Property(e => e.Umcid).HasColumnName("UMCID");
            entity.Property(e => e.CauseName)
                .HasMaxLength(50)
                .HasColumnName("CAUSE_NAME");
            entity.Property(e => e.Remarks)
                .HasMaxLength(50)
                .HasColumnName("REMARKS");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("STATUS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
