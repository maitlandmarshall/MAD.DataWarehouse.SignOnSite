﻿// <auto-generated />
using System;
using MAD.DataWarehouse.SignOnSite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MAD.DataWarehouse.SignOnSite.Migrations
{
    [DbContext(typeof(SignOnSiteDbContext))]
    [Migration("20210908004654_AddTable_SiteUser")]
    partial class AddTable_SiteUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MAD.DataWarehouse.SignOnSite.Api.BriefingLog", b =>
                {
                    b.Property<int>("BriefingId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("Day")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("EarliestAcknowledgedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("EarliestSeenAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("BriefingId", "FirstName", "LastName", "Day", "EarliestAcknowledgedAt");

                    b.ToTable("BriefingLog");
                });

            modelBuilder.Entity("MAD.DataWarehouse.SignOnSite.Api.Site", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("InternalReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Site");
                });

            modelBuilder.Entity("MAD.DataWarehouse.SignOnSite.Api.SiteAttendance", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInductedToSite")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisitor")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("SignoffAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("SignonAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("SiteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("SiteAttendance");
                });

            modelBuilder.Entity("MAD.DataWarehouse.SignOnSite.Api.SiteBriefing", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("AcknowledgementCount")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("SeenCount")
                        .HasColumnType("int");

                    b.Property<bool>("ShouldShowSetByUser")
                        .HasColumnType("bit");

                    b.Property<int>("SiteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("SiteBriefing");
                });

            modelBuilder.Entity("MAD.DataWarehouse.SignOnSite.Api.SiteUser", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("SiteId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "SiteId");

                    b.HasIndex("SiteId");

                    b.ToTable("SiteUser");
                });

            modelBuilder.Entity("MAD.DataWarehouse.SignOnSite.Api.BriefingLog", b =>
                {
                    b.HasOne("MAD.DataWarehouse.SignOnSite.Api.SiteBriefing", "Briefing")
                        .WithMany()
                        .HasForeignKey("BriefingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("MAD.DataWarehouse.SignOnSite.Api.BriefingLog+BLCompany", "Company", b1 =>
                        {
                            b1.Property<int>("BriefingLogBriefingId")
                                .HasColumnType("int");

                            b1.Property<string>("BriefingLogFirstName")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("BriefingLogLastName")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTimeOffset>("BriefingLogDay")
                                .HasColumnType("datetimeoffset");

                            b1.Property<DateTimeOffset>("BriefingLogEarliestAcknowledgedAt")
                                .HasColumnType("datetimeoffset");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BriefingLogBriefingId", "BriefingLogFirstName", "BriefingLogLastName", "BriefingLogDay", "BriefingLogEarliestAcknowledgedAt");

                            b1.ToTable("BriefingLog");

                            b1.WithOwner()
                                .HasForeignKey("BriefingLogBriefingId", "BriefingLogFirstName", "BriefingLogLastName", "BriefingLogDay", "BriefingLogEarliestAcknowledgedAt");
                        });

                    b.Navigation("Briefing");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("MAD.DataWarehouse.SignOnSite.Api.SiteAttendance", b =>
                {
                    b.HasOne("MAD.DataWarehouse.SignOnSite.Api.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("MAD.DataWarehouse.SignOnSite.Api.SiteAttendance+SACompany", "Company", b1 =>
                        {
                            b1.Property<int>("SiteAttendanceId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("SiteAttendanceId");

                            b1.ToTable("SiteAttendance");

                            b1.WithOwner()
                                .HasForeignKey("SiteAttendanceId");
                        });

                    b.OwnsOne("MAD.DataWarehouse.SignOnSite.Api.SiteAttendance+SAUser", "User", b1 =>
                        {
                            b1.Property<int>("SiteAttendanceId")
                                .HasColumnType("int");

                            b1.Property<string>("Email")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("FirstName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.Property<string>("LastName")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("SiteAttendanceId");

                            b1.ToTable("SiteAttendance");

                            b1.WithOwner()
                                .HasForeignKey("SiteAttendanceId");
                        });

                    b.Navigation("Company");

                    b.Navigation("Site");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MAD.DataWarehouse.SignOnSite.Api.SiteBriefing", b =>
                {
                    b.HasOne("MAD.DataWarehouse.SignOnSite.Api.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("MAD.DataWarehouse.SignOnSite.Api.SiteUser", b =>
                {
                    b.HasOne("MAD.DataWarehouse.SignOnSite.Api.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("MAD.DataWarehouse.SignOnSite.Api.SiteUser+SUInduction", "Induction", b1 =>
                        {
                            b1.Property<int>("SiteUserId")
                                .HasColumnType("int");

                            b1.Property<int>("SiteUserSiteId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.Property<int>("Number")
                                .HasColumnType("int");

                            b1.Property<string>("Type")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("SiteUserId", "SiteUserSiteId");

                            b1.ToTable("SiteUser");

                            b1.WithOwner()
                                .HasForeignKey("SiteUserId", "SiteUserSiteId");

                            b1.OwnsOne("MAD.DataWarehouse.SignOnSite.Api.SiteUser+SUInduction+InductionState", "State", b2 =>
                                {
                                    b2.Property<int>("SUInductionSiteUserId")
                                        .HasColumnType("int");

                                    b2.Property<int>("SUInductionSiteUserSiteId")
                                        .HasColumnType("int");

                                    b2.Property<string>("AsString")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<DateTime>("SetAt")
                                        .HasColumnType("date");

                                    b2.HasKey("SUInductionSiteUserId", "SUInductionSiteUserSiteId");

                                    b2.ToTable("SiteUser");

                                    b2.WithOwner()
                                        .HasForeignKey("SUInductionSiteUserId", "SUInductionSiteUserSiteId");

                                    b2.OwnsOne("MAD.DataWarehouse.SignOnSite.Api.SiteUser+SUInduction+InductionState+StateSetBy", "SetBy", b3 =>
                                        {
                                            b3.Property<int>("InductionStateSUInductionSiteUserId")
                                                .HasColumnType("int");

                                            b3.Property<int>("InductionStateSUInductionSiteUserSiteId")
                                                .HasColumnType("int");

                                            b3.Property<string>("FirstName")
                                                .HasColumnType("nvarchar(max)");

                                            b3.Property<int>("Id")
                                                .HasColumnType("int");

                                            b3.Property<string>("LastName")
                                                .HasColumnType("nvarchar(max)");

                                            b3.HasKey("InductionStateSUInductionSiteUserId", "InductionStateSUInductionSiteUserSiteId");

                                            b3.ToTable("SiteUser");

                                            b3.WithOwner()
                                                .HasForeignKey("InductionStateSUInductionSiteUserId", "InductionStateSUInductionSiteUserSiteId");
                                        });

                                    b2.Navigation("SetBy");
                                });

                            b1.Navigation("State");
                        });

                    b.OwnsOne("MAD.DataWarehouse.SignOnSite.Api.SiteUser+SUSiteCompany", "SiteCompany", b1 =>
                        {
                            b1.Property<int>("SiteUserId")
                                .HasColumnType("int");

                            b1.Property<int>("SiteUserSiteId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("SiteUserId", "SiteUserSiteId");

                            b1.ToTable("SiteUser");

                            b1.WithOwner()
                                .HasForeignKey("SiteUserId", "SiteUserSiteId");
                        });

                    b.Navigation("Induction");

                    b.Navigation("Site");

                    b.Navigation("SiteCompany");
                });
#pragma warning restore 612, 618
        }
    }
}
