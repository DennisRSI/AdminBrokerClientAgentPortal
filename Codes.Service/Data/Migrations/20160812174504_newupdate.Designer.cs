using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Codes.Service.Data;

namespace Codes.Service.Data.Migrations
{
    [DbContext(typeof(CodesDbContext))]
    [Migration("20160812174504_newupdate")]
    partial class newupdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Codes.Service.Models.BulkCodeAuditModel", b =>
                {
                    b.Property<int>("BulkCodeAuditId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CreatorIP")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<string>("Errors");

                    b.Property<DateTime?>("FinishDate");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Issuer")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("OrigionalFileSent");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("TotalFailed");

                    b.Property<int>("TotalProcessed");

                    b.Property<int>("TotalSent");

                    b.Property<int>("TotalSucceeded");

                    b.HasKey("BulkCodeAuditId");

                    b.ToTable("BulkCodeAudits");
                });

            modelBuilder.Entity("Codes.Service.Models.CodeActivityModel", b =>
                {
                    b.Property<int>("CodeActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActivationCode")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("Address1")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Address2")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("City")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("CodeId");

                    b.Property<string>("CountryCode")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CreatorIP")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("EmailVerifiedDate");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Issuer")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("MiddleName")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Phone1")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("Phone2")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("PostalCode")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("RSIId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("StateCode")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("CodeActivityId");

                    b.HasIndex("CodeId");

                    b.HasIndex("RSIId");

                    b.ToTable("CodeActivities");
                });

            modelBuilder.Entity("Codes.Service.Models.CodeModel", b =>
                {
                    b.Property<int>("CodeId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("ChargeAmount");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 500);

                    b.Property<int>("CondoRewards");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CreatorIP")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<DateTime?>("EndDate");

                    b.Property<int>("HotelPoints");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Issuer")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("IssuerReference")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("NumberOfUses");

                    b.Property<int>("PackageId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime?>("StartDate");

                    b.Property<bool>("VerifyEmail");

                    b.HasKey("CodeId");

                    b.HasIndex("Issuer");

                    b.ToTable("Codes");
                });

            modelBuilder.Entity("Codes.Service.Models.CodeActivityModel", b =>
                {
                    b.HasOne("Codes.Service.Models.CodeModel", "Code")
                        .WithMany("CodeActivities")
                        .HasForeignKey("CodeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
