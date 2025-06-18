using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes1.Service.Data.Migrations
{
    [DbContext(typeof(Codes1DbContext))]
    [Migration("20160805181245_UpdateIndex1")]
    partial class UpdateIndex1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CodeGenerator.Data.Models.CodeActivityModel", b =>
                {
                    b.Property<int>("CodeActivityId")
                        .ValueGeneratedOnAdd();

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

                    b.HasIndex("RSIId");

                    b.ToTable("CodeActivities");
                });

            modelBuilder.Entity("CodeGenerator.Data.Models.CodeModel", b =>
                {
                    b.Property<string>("Code");

                    b.Property<decimal>("ChargeAmount");

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

                    b.HasKey("Code");

                    b.HasIndex("Issuer");

                    b.ToTable("Codes");
                });
        }
    }
}
