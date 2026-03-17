using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPAMatchineTrack.Migrations
{
    /// <inheritdoc />
    public partial class InitialIdentitySetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Brand_Information",
                columns: table => new
                {
                    BRID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Brand_Information", x => x.BRID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Company_Info",
                columns: table => new
                {
                    CID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Short_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Company_Info", x => x.CID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Extra_MC_Requisition",
                columns: table => new
                {
                    RID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RDATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CID = table.Column<int>(type: "int", nullable: true),
                    BOOKING_NO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    REQUIRED_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    BUID = table.Column<int>(type: "int", nullable: true),
                    FID = table.Column<int>(type: "int", nullable: true),
                    LID = table.Column<int>(type: "int", nullable: true),
                    REQ_FOR = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PREPARE_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CHECKED_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    AUTH_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    OPT1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OPT2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OPT3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Extra_MC_Requisition", x => x.RID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Machine_Type_Info",
                columns: table => new
                {
                    MTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Machine_Type_Info", x => x.MTID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Other_Company",
                columns: table => new
                {
                    OCID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OC_NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CONTRACT_PERSON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    REMARKS = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Other_Company", x => x.OCID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Rent_MC_Req_D",
                columns: table => new
                {
                    TRNSID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TRNSDATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    RID = table.Column<int>(type: "int", nullable: true),
                    MTID = table.Column<int>(type: "int", nullable: true),
                    QTY = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    REMARKS = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Rent_MC_Req_D", x => x.TRNSID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Scan_Information",
                columns: table => new
                {
                    SCID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SCDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Lid = table.Column<int>(type: "int", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barcode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Scan_Information", x => x.SCID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Building_Info",
                columns: table => new
                {
                    BUID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CID = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Building_Info", x => x.BUID);
                    table.ForeignKey(
                        name: "FK_tbl_Building_Info_tbl_Company_Info",
                        column: x => x.CID,
                        principalTable: "tbl_Company_Info",
                        principalColumn: "CID");
                });

            migrationBuilder.CreateTable(
                name: "tbl_Rent_MC_Requisition",
                columns: table => new
                {
                    RID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RDATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CID = table.Column<int>(type: "int", nullable: true),
                    BOOKING_NO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    REQUIRED_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    BUID = table.Column<int>(type: "int", nullable: true),
                    FID = table.Column<int>(type: "int", nullable: true),
                    LID = table.Column<int>(type: "int", nullable: true),
                    REQ_FOR = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PREPARE_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CHECKED_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    AUTH_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    OPT1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OPT2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OPT3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Rent_MC_Requisition", x => x.RID);
                    table.ForeignKey(
                        name: "FK_tbl_Rent_MC_Requisition_tbl_Company_Info_CID",
                        column: x => x.CID,
                        principalTable: "tbl_Company_Info",
                        principalColumn: "CID");
                });

            migrationBuilder.CreateTable(
                name: "tbl_Extra_MC_Req_D",
                columns: table => new
                {
                    TRNSID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TRNSDATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    RID = table.Column<int>(type: "int", nullable: true),
                    MTID = table.Column<int>(type: "int", nullable: true),
                    QTY = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    Exist_Qty = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    Capacity = table.Column<decimal>(type: "numeric(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Extra_MC_Req_D", x => x.TRNSID);
                    table.ForeignKey(
                        name: "FK_tbl_Extra_MC_Req_D_tbl_Extra_MC_Requisition",
                        column: x => x.RID,
                        principalTable: "tbl_Extra_MC_Requisition",
                        principalColumn: "RID");
                });

            migrationBuilder.CreateTable(
                name: "tbl_Machine_Details",
                columns: table => new
                {
                    MCID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CID = table.Column<int>(type: "int", nullable: true),
                    MCNO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MTID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BRID = table.Column<int>(type: "int", nullable: true),
                    Model = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SRNO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Rcv_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Capaity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P_System = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Machine_Details", x => x.MCID);
                    table.ForeignKey(
                        name: "FK_tbl_Machine_Details_tbl_Brand_Information_BRID",
                        column: x => x.BRID,
                        principalTable: "tbl_Brand_Information",
                        principalColumn: "BRID");
                    table.ForeignKey(
                        name: "FK_tbl_Machine_Details_tbl_Company_Info_CID",
                        column: x => x.CID,
                        principalTable: "tbl_Company_Info",
                        principalColumn: "CID");
                    table.ForeignKey(
                        name: "FK_tbl_Machine_Details_tbl_Machine_Type_Info_MTID",
                        column: x => x.MTID,
                        principalTable: "tbl_Machine_Type_Info",
                        principalColumn: "MTID");
                });

            migrationBuilder.CreateTable(
                name: "tbl_Layout",
                columns: table => new
                {
                    SLNO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    MCID = table.Column<int>(type: "int", nullable: true),
                    LID = table.Column<int>(type: "int", nullable: true),
                    LOCATION_DETAILS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STATUS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OCID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Layout", x => x.SLNO);
                    table.ForeignKey(
                        name: "FK_tbl_Layout_tbl_Other_Company_OCID",
                        column: x => x.OCID,
                        principalTable: "tbl_Other_Company",
                        principalColumn: "OCID");
                });

            migrationBuilder.CreateTable(
                name: "tbl_Floor_Info",
                columns: table => new
                {
                    FID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CID = table.Column<int>(type: "int", nullable: true),
                    BUID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Floor_Info", x => x.FID);
                    table.ForeignKey(
                        name: "FK_tbl_Floor_Info_tbl_Building_Info_BUID",
                        column: x => x.BUID,
                        principalTable: "tbl_Building_Info",
                        principalColumn: "BUID");
                    table.ForeignKey(
                        name: "FK_tbl_Floor_Info_tbl_Company_Info_CID",
                        column: x => x.CID,
                        principalTable: "tbl_Company_Info",
                        principalColumn: "CID");
                });

            migrationBuilder.CreateTable(
                name: "tbl_MC_Location",
                columns: table => new
                {
                    LID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cid = table.Column<int>(type: "int", nullable: true),
                    Buid = table.Column<int>(type: "int", nullable: true),
                    Fid = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FidNavigationFid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_MC_Location", x => x.LID);
                    table.ForeignKey(
                        name: "FK_tbl_MC_Location_tbl_Building_Info_Buid",
                        column: x => x.Buid,
                        principalTable: "tbl_Building_Info",
                        principalColumn: "BUID");
                    table.ForeignKey(
                        name: "FK_tbl_MC_Location_tbl_Company_Info_Cid",
                        column: x => x.Cid,
                        principalTable: "tbl_Company_Info",
                        principalColumn: "CID");
                    table.ForeignKey(
                        name: "FK_tbl_MC_Location_tbl_Floor_Info_FidNavigationFid",
                        column: x => x.FidNavigationFid,
                        principalTable: "tbl_Floor_Info",
                        principalColumn: "FID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Building_Info_CID",
                table: "tbl_Building_Info",
                column: "CID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Extra_MC_Req_D_RID",
                table: "tbl_Extra_MC_Req_D",
                column: "RID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Floor_Info_BUID",
                table: "tbl_Floor_Info",
                column: "BUID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Floor_Info_CID",
                table: "tbl_Floor_Info",
                column: "CID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Layout_OCID",
                table: "tbl_Layout",
                column: "OCID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Machine_Details_BRID",
                table: "tbl_Machine_Details",
                column: "BRID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Machine_Details_CID",
                table: "tbl_Machine_Details",
                column: "CID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Machine_Details_MTID",
                table: "tbl_Machine_Details",
                column: "MTID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MC_Location_Buid",
                table: "tbl_MC_Location",
                column: "Buid");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MC_Location_Cid",
                table: "tbl_MC_Location",
                column: "Cid");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MC_Location_FidNavigationFid",
                table: "tbl_MC_Location",
                column: "FidNavigationFid");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Rent_MC_Requisition_CID",
                table: "tbl_Rent_MC_Requisition",
                column: "CID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "tbl_Extra_MC_Req_D");

            migrationBuilder.DropTable(
                name: "tbl_Layout");

            migrationBuilder.DropTable(
                name: "tbl_Machine_Details");

            migrationBuilder.DropTable(
                name: "tbl_MC_Location");

            migrationBuilder.DropTable(
                name: "tbl_Rent_MC_Req_D");

            migrationBuilder.DropTable(
                name: "tbl_Rent_MC_Requisition");

            migrationBuilder.DropTable(
                name: "tbl_Scan_Information");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tbl_Extra_MC_Requisition");

            migrationBuilder.DropTable(
                name: "tbl_Other_Company");

            migrationBuilder.DropTable(
                name: "tbl_Brand_Information");

            migrationBuilder.DropTable(
                name: "tbl_Machine_Type_Info");

            migrationBuilder.DropTable(
                name: "tbl_Floor_Info");

            migrationBuilder.DropTable(
                name: "tbl_Building_Info");

            migrationBuilder.DropTable(
                name: "tbl_Company_Info");
        }
    }
}
