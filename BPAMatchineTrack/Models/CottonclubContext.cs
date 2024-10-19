using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BPAMatchineTrack.Models;

public partial class CottonclubContext : IdentityDbContext<ApplicationUser>
{
    public CottonclubContext()
    {
    }

    public CottonclubContext(DbContextOptions<CottonclubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBuildingInfo> TblBuildingInfos { get; set; }

    public virtual DbSet<TblCompanyInfo> TblCompanyInfos { get; set; }

    public virtual DbSet<TblFloorInfo> TblFloorInfos { get; set; }

   // public virtual DbSet<TblMachineType> TblMachineTypes { get; set; }

    public virtual DbSet<tbl_Machine_Detail> tbl_Machine_Details { get; set; }

    public virtual DbSet<TblMcLocation> TblMcLocations { get; set; }

    public virtual DbSet<TblScanInformation> TblScanInformations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserLogin<string>>()
            .HasKey(u => new { u.UserId, u.LoginProvider, u.ProviderKey });

        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<IdentityUserToken<string>>()
    .HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

      //  modelBuilder.Entity<tbl_Rent_MC_Requisition>()
      //.HasOne(r => r.TblCompanyInfo)
      //.WithMany() // Assuming one company can have many requisitions
      //.HasForeignKey(r => r.CID);

        modelBuilder.Entity<TblBuildingInfo>(entity =>
        {
            entity.HasKey(e => e.Buid);

            entity.ToTable("tbl_Building_Info");

            entity.Property(e => e.Buid).HasColumnName("BUID");
            entity.Property(e => e.Cid).HasColumnName("CID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Opt1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Opt2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Opt3)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remarks)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CidNavigation).WithMany(p => p.TblBuildingInfos)
                .HasForeignKey(d => d.Cid)
                .HasConstraintName("FK_tbl_Building_Info_tbl_Company_Info");
        });

        modelBuilder.Entity<TblCompanyInfo>(entity =>
        {
            entity.HasKey(e => e.Cid);

            entity.ToTable("tbl_Company_Info");

            entity.Property(e => e.Cid).HasColumnName("CID");
            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Company_Name");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Opt1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Opt2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Opt3)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Opt4)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Opt5)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remarks)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ShortName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Short_Name");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblFloorInfo>(entity =>
        {
            entity.HasKey(e => e.Fid);

            entity.ToTable("tbl_Floor_Info");

            entity.Property(e => e.Fid).HasColumnName("FID");
            entity.Property(e => e.Buid).HasColumnName("BUID");
            entity.Property(e => e.Cid).HasColumnName("CID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remarks)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Bu).WithMany(p => p.TblFloorInfos)
                .HasForeignKey(d => d.Buid)
                .HasConstraintName("FK_tbl_Floor_Info_tbl_Building_Info");

            entity.HasOne(d => d.CidNavigation).WithMany(p => p.TblFloorInfos)
                .HasForeignKey(d => d.Cid)
                .HasConstraintName("FK_tbl_Floor_Info_tbl_Company_Info");
        });


        modelBuilder.Entity<tbl_Machine_Detail>(entity =>
        {
            entity.HasKey(e => e.MCID);

            entity.Property(e => e.MCNO)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remarks)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SRNO)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.BR).WithMany(p => p.tbl_Machine_Details)
                .HasForeignKey(d => d.BRID)
                .HasConstraintName("FK_tbl_Machine_Details_tbl_Brand_Information");

            entity.HasOne(d => d.CIDNavigation).WithMany(p => p.tbl_Machine_Details)
                .HasForeignKey(d => d.CID)
                .HasConstraintName("FK_tbl_Machine_Details_tbl_Company_Info");

            entity.HasOne(d => d.MT).WithMany(p => p.tbl_Machine_Details)
                .HasForeignKey(d => d.MTID)
                .HasConstraintName("FK_tbl_Machine_Details_tbl_Machine_Type_Info");
        });

        modelBuilder.Entity<TblMcLocation>(entity =>
        {
            entity.HasKey(e => e.Lid);

            entity.ToTable("tbl_MC_Location");

            entity.Property(e => e.Lid).HasColumnName("LID");
            entity.Property(e => e.Buid).HasColumnName("BUID");
            entity.Property(e => e.Cid).HasColumnName("CID");
            entity.Property(e => e.Fid).HasColumnName("FID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Opt1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Opt2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Opt3)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remarks)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Bu).WithMany(p => p.TblMcLocations)
                .HasForeignKey(d => d.Buid)
                .HasConstraintName("FK_tbl_MC_Location_tbl_Building_Info");

            entity.HasOne(d => d.CidNavigation).WithMany(p => p.TblMcLocations)
                .HasForeignKey(d => d.Cid)
                .HasConstraintName("FK_tbl_MC_Location_tbl_Company_Info");

            entity.HasOne(d => d.FidNavigation).WithMany(p => p.TblMcLocations)
                .HasForeignKey(d => d.Fid)
                .HasConstraintName("FK_tbl_MC_Location_tbl_Floor_Info");
        });

        modelBuilder.Entity<TblScanInformation>(entity =>
        {
            entity.HasKey(e => e.Scid);

            entity.ToTable("tbl_Scan_Information");

            entity.Property(e => e.Scid)
                .ValueGeneratedOnAdd()
                .HasColumnName("SCID");
            entity.Property(e => e.Barcode)
                .HasMaxLength(50)
                .IsUnicode(false);
            //entity.Property(e => e.Buid).HasColumnName("BUID");
            //entity.Property(e => e.Cid).HasColumnName("CID");
            //entity.Property(e => e.Fid).HasColumnName("FID");
            entity.Property(e => e.Lid).HasColumnName("LID");
            entity.Property(e => e.Opt1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Opt2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Opt3)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remarks)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Scdate)
                .HasColumnType("datetime")
                .HasColumnName("SCDate");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });
        modelBuilder.Entity<TblBrandInformation>(entity =>
        {
            entity.HasKey(e => e.Brid);

            entity.ToTable("tbl_Brand_Information");

            entity.Property(e => e.Brid).HasColumnName("BRID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblMachineTypeInfo>(entity =>
        {
            entity.HasKey(e => e.Mtid);

            entity.ToTable("tbl_Machine_Type_Info");

            entity.Property(e => e.Mtid).HasColumnName("MTID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });
        modelBuilder.Entity<tbl_Rent_MC_Req_D>(entity =>
        {
            entity.HasKey(e => e.TRNSID);

            entity.ToTable("tbl_Rent_MC_Req_D");

            entity.Property(e => e.QTY).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.REMARKS)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TRNSDATE).HasColumnType("datetime");
        });

        modelBuilder.Entity<tbl_Rent_MC_Requisition>(entity =>
        {
            entity.HasKey(e => e.RID);

            entity.ToTable("tbl_Rent_MC_Requisition");

            entity.Property(e => e.AUTH_BY)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BOOKING_NO)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CHECKED_BY)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OPT1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OPT2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OPT3)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PREPARE_BY)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RDATE).HasColumnType("datetime");
            entity.Property(e => e.REQUIRED_DATE).HasColumnType("datetime");
            entity.Property(e => e.REQ_FOR)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<TblBrandInformation> TblBrandInformation { get; set; } = default!;

    public DbSet<TblMachineTypeInfo> TblMachineTypeInfo { get; set; } = default!;
    public DbSet<tbl_Rent_MC_Requisition> tbl_Rent_MC_Requisition { get; set; }
    public DbSet<tbl_Rent_MC_Req_D> tbl_Rent_MC_Req_D { get; set; }

}
