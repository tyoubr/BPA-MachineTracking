using BPAMachineTrack.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BPAMatchineTrack.Models
{
    public partial class CottonclubContext : IdentityDbContext<IdentityUser>
    {
        public CottonclubContext(DbContextOptions<CottonclubContext> options)
            : base(options)
        {
        }

        // ==============================
        // YOUR TABLES
        // ==============================

        public DbSet<TblBuildingInfo> TblBuildingInfos { get; set; }
        public DbSet<TblCompanyInfo> TblCompanyInfos { get; set; }
        public DbSet<TblFloorInfo> TblFloorInfos { get; set; }
        public DbSet<tbl_Machine_Detail> tbl_Machine_Details { get; set; }
        public DbSet<TblMcLocation> TblMcLocations { get; set; }
        public DbSet<TblScanInformation> TblScanInformations { get; set; }

        public DbSet<TblBrandInformation> TblBrandInformation { get; set; }
        public DbSet<TblMachineTypeInfo> TblMachineTypeInfo { get; set; }

        public DbSet<tbl_Rent_MC_Requisition> tbl_Rent_MC_Requisition { get; set; }
        public DbSet<tbl_Rent_MC_Req_D> tbl_Rent_MC_Req_D { get; set; }

        public DbSet<tbl_Extra_MC_Req_D> tbl_Extra_MC_Req_Ds { get; set; }
        public DbSet<tbl_Extra_MC_Requisition> tbl_Extra_MC_Requisitions { get; set; }

        public DbSet<tbl_Layout> tbl_Layouts { get; set; }
        public DbSet<tbl_Other_Company> tbl_Other_Companies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // VERY IMPORTANT FOR ASP.NET IDENTITY
            base.OnModelCreating(modelBuilder);


            // ==============================
            // Building
            // ==============================

            modelBuilder.Entity<TblBuildingInfo>(entity =>
            {
                entity.HasKey(e => e.Buid);

                entity.ToTable("tbl_Building_Info");

                entity.Property(e => e.Buid).HasColumnName("BUID");
                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CidNavigation)
                    .WithMany(p => p.TblBuildingInfos)
                    .HasForeignKey(d => d.Cid)
                    .HasConstraintName("FK_tbl_Building_Info_tbl_Company_Info");
            });


            // ==============================
            // Company
            // ==============================

            modelBuilder.Entity<TblCompanyInfo>(entity =>
            {
                entity.HasKey(e => e.Cid);

                entity.ToTable("tbl_Company_Info");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.CompanyName)
                    .HasColumnName("Company_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShortName)
                    .HasColumnName("Short_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });


            // ==============================
            // Floor
            // ==============================

            modelBuilder.Entity<TblFloorInfo>(entity =>
            {
                entity.HasKey(e => e.Fid);

                entity.ToTable("tbl_Floor_Info");

                entity.Property(e => e.Fid).HasColumnName("FID");
                entity.Property(e => e.Buid).HasColumnName("BUID");
                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.HasOne(d => d.Bu)
                    .WithMany(p => p.TblFloorInfos)
                    .HasForeignKey(d => d.Buid);

                entity.HasOne(d => d.CidNavigation)
                    .WithMany(p => p.TblFloorInfos)
                    .HasForeignKey(d => d.Cid);
            });


            // ==============================
            // Machine Details
            // ==============================

            modelBuilder.Entity<tbl_Machine_Detail>(entity =>
            {
                entity.HasKey(e => e.MCID);

                entity.Property(e => e.MCNO)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SRNO)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.IsRental)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IS_RENTAL");
                entity.HasOne(d => d.CIDNavigation)
                    .WithMany(p => p.tbl_Machine_Details)
                    .HasForeignKey(d => d.CID);
            });


            // ==============================
            // Machine Location
            // ==============================

            modelBuilder.Entity<TblMcLocation>(entity =>
            {
                entity.HasKey(e => e.Lid);

                entity.ToTable("tbl_MC_Location");

                entity.Property(e => e.Lid).HasColumnName("LID");

                entity.HasOne(d => d.CidNavigation)
                    .WithMany(p => p.TblMcLocations)
                    .HasForeignKey(d => d.Cid);
            });


            // ==============================
            // Scan Info
            // ==============================

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

                entity.Property(e => e.Scdate)
                    .HasColumnName("SCDate")
                    .HasColumnType("datetime");
            });


            // ==============================
            // Brand
            // ==============================

            modelBuilder.Entity<TblBrandInformation>(entity =>
            {
                entity.HasKey(e => e.Brid);

                entity.ToTable("tbl_Brand_Information");

                entity.Property(e => e.Brid).HasColumnName("BRID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });


            // ==============================
            // Machine Type
            // ==============================

            modelBuilder.Entity<TblMachineTypeInfo>(entity =>
            {
                entity.HasKey(e => e.Mtid);

                entity.ToTable("tbl_Machine_Type_Info");

                entity.Property(e => e.Mtid).HasColumnName("MTID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });


            // ==============================
            // Layout
            // ==============================

            modelBuilder.Entity<tbl_Layout>(entity =>
            {
                entity.HasKey(e => e.SLNO);

                entity.ToTable("tbl_Layout");

                entity.Property(e => e.DATE)
                    .HasColumnType("datetime");
            });


            // ==============================
            // Other Company
            // ==============================

            modelBuilder.Entity<tbl_Other_Company>(entity =>
            {
                entity.HasKey(e => e.OCID);

                entity.ToTable("tbl_Other_Company");

                entity.Property(e => e.OC_NAME)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<tbl_Extra_MC_Req_D>(entity =>
            {
                entity.HasKey(e => e.TRNSID);

                entity.ToTable("tbl_Extra_MC_Req_D");

                entity.Property(e => e.Capacity).HasColumnType("numeric(18,0)");
                entity.Property(e => e.Exist_Qty).HasColumnType("numeric(18,0)");
                entity.Property(e => e.QTY).HasColumnType("numeric(18,0)");
                entity.Property(e => e.TRNSDATE).HasColumnType("datetime");

                entity.HasOne(d => d.RIDNavigation)
                    .WithMany(p => p.tbl_Extra_MC_Req_Ds)
                    .HasForeignKey(d => d.RID)
                    .HasConstraintName("FK_tbl_Extra_MC_Req_D_tbl_Extra_MC_Requisition");
            });
            modelBuilder.Entity<tbl_Extra_MC_Requisition>(entity =>
            {
                entity.HasKey(e => e.RID);

                entity.ToTable("tbl_Extra_MC_Requisition");

                entity.Property(e => e.AUTH_BY)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BOOKING_NO)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CHECKED_BY)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PREPARE_BY)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RDATE)
                    .HasColumnType("datetime");

                entity.Property(e => e.REQUIRED_DATE)
                    .HasColumnType("datetime");

                entity.Property(e => e.REQ_FOR)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<tbl_Rent_MC_Req_D>(entity =>
            {
                entity.HasKey(e => e.TRNSID);

                entity.ToTable("tbl_Rent_MC_Req_D");

                entity.Property(e => e.QTY)
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.REMARKS)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TRNSDATE)
                    .HasColumnType("datetime");
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

                entity.Property(e => e.PREPARE_BY)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RDATE)
                    .HasColumnType("datetime");

                entity.Property(e => e.REQUIRED_DATE)
                    .HasColumnType("datetime");

                entity.Property(e => e.REQ_FOR)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}