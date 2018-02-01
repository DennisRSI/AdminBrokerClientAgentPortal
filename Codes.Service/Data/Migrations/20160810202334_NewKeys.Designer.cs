using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Codes.Service.Data;

namespace Codes.Service.Data.Migrations
{
    [DbContext(typeof(CodesDbContext))]
    [Migration("20160810202334_NewKeys")]
    partial class NewKeys
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

            modelBuilder.Entity("Codes.Service.Models.CodeActivityModel", b =>
                {
                    b.Property<int>("CodeActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CodeModelCodeId");

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

                    b.HasIndex("CodeModelCodeId");

                    b.HasIndex("RSIId");

                    b.ToTable("CodeActivities");
                });

            modelBuilder.Entity("Codes.Service.Models.CodeModel", b =>
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

            modelBuilder.Entity("Codes.Service.Models.CodeActivityModel", b =>
                {
                    b.HasOne("Codes.Service.Models.CodeModel")
                        .WithMany("CodeActivities")
                        .HasForeignKey("CodeModelCodeId");
                });
        }
    }
}
