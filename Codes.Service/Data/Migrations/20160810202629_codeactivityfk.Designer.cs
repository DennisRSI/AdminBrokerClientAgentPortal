﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Codes1.Service.Data;

namespace Codes1.Service.Data.Migrations
{
    [DbContext(typeof(Codes1DbContext))]
    [Migration("20160810202629_codeactivityfk")]
    partial class codeactivityfk
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Codes1.Service.Models.BulkCodeAuditModel", b =>
                {
                    b.Property<int>("BulkCodeAuditId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CreatorIP")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Errors");

                    b.Property<DateTime?>("FinishDate");

                    b.Property<string>("Issuer")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("OrigionalFileSent");

                    b.Property<int>("TotalFailed");

                    b.Property<int>("TotalProcessed");

                    b.Property<int>("TotalSent");

                    b.Property<int>("TotalSucceeded");

                    b.HasKey("BulkCodeAuditId");

                    b.ToTable("BulkCodeAudits");
                });

            modelBuilder.Entity("Codes1.Service.Models.CodeActivityModel", b =>
                {
                    b.Property<int>("CodeActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CodeId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CreatorIP")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<bool>("IsActive");

                    b.Property<int>("RSIId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("CodeActivityId");

                    b.HasIndex("CodeId");

                    b.HasIndex("RSIId");

                    b.ToTable("CodeActivities");
                });

            modelBuilder.Entity("Codes1.Service.Models.CodeModel", b =>
                {
                    b.Property<int>("CodeId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("ChargeAmount");

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<DateTime?>("EndDate");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Issuer")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("IssuerReference")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("NumberOfUses");

                    b.Property<int>("Points");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime?>("StartDate");

                    b.HasKey("CodeId");

                    b.HasIndex("Issuer");

                    b.ToTable("Codes");
                });

            modelBuilder.Entity("Codes1.Service.Models.CodeActivityModel", b =>
                {
                    b.HasOne("Codes1.Service.Models.CodeModel", "Code")
                        .WithMany("CodeActivities")
                        .HasForeignKey("CodeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
