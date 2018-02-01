using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Codes.Service.Data;

namespace Codes.Service.Data.Migrations
{
    [DbContext(typeof(CodesDbContext))]
    [Migration("20171218221024_DeactivationReason1")]
    partial class DeactivationReason1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Codes.Service.Models.AdditionalCodeActivityModel", b =>
                {
                    b.Property<int>("AdditionalCodeActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActivationCode")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("CodeId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CreatorIP")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<bool>("IsActive");

                    b.Property<int>("RSIId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("AdditionalCodeActivityId");

                    b.ToTable("AdditionalCodeActivities");
                });

            modelBuilder.Entity("Codes.Service.Models.AgentModel", b =>
                {
                    b.Property<int>("AgentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(255);

                    b.Property<string>("AgentFirstName")
                        .HasMaxLength(255);

                    b.Property<string>("AgentLastName")
                        .HasMaxLength(255);

                    b.Property<int>("BrokerId");

                    b.Property<string>("City")
                        .HasMaxLength(100);

                    b.Property<float>("CommissionRate");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CreatorIP")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<DateTime?>("DeactivationReason");

                    b.Property<string>("EIN")
                        .HasMaxLength(500);

                    b.Property<string>("Fax")
                        .HasMaxLength(50);

                    b.Property<string>("FaxExtension")
                        .HasMaxLength(50);

                    b.Property<bool>("IsActive");

                    b.Property<string>("MobilePhone")
                        .HasMaxLength(50);

                    b.Property<string>("OfficeExtension")
                        .HasMaxLength(50);

                    b.Property<string>("OfficePhone")
                        .HasMaxLength(50);

                    b.Property<string>("PostalCode")
                        .HasMaxLength(50);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("State")
                        .HasMaxLength(100);

                    b.HasKey("AgentId");

                    b.HasIndex("BrokerId");

                    b.ToTable("Agents");
                });

            modelBuilder.Entity("Codes.Service.Models.BrokerModel", b =>
                {
                    b.Property<int>("BrokerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(255);

                    b.Property<string>("BrokerFirstName")
                        .HasMaxLength(255);

                    b.Property<string>("BrokerLastName")
                        .HasMaxLength(255);

                    b.Property<string>("City")
                        .HasMaxLength(100);

                    b.Property<string>("CompanyName")
                        .HasMaxLength(500);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CreatorIP")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<DateTime?>("DeactivationReason");

                    b.Property<string>("EIN")
                        .HasMaxLength(500);

                    b.Property<string>("Fax")
                        .HasMaxLength(50);

                    b.Property<string>("FaxExtension")
                        .HasMaxLength(50);

                    b.Property<bool>("IsActive");

                    b.Property<string>("MobilePhone")
                        .HasMaxLength(50);

                    b.Property<string>("OfficeExtension")
                        .HasMaxLength(50);

                    b.Property<string>("OfficePhone")
                        .HasMaxLength(50);

                    b.Property<int>("ParentBrokerId");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(50);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("State")
                        .HasMaxLength(100);

                    b.HasKey("BrokerId");

                    b.ToTable("Brokers");
                });

            modelBuilder.Entity("Codes.Service.Models.BulkCodeAuditModel", b =>
                {
                    b.Property<int>("BulkCodeAuditId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CreatorIP")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<string>("Errors");

                    b.Property<DateTime?>("FinishDate");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Issuer")
                        .IsRequired()
                        .HasMaxLength(50);

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

            modelBuilder.Entity("Codes.Service.Models.CampaignAgentModel", b =>
                {
                    b.Property<int>("CampaignId");

                    b.Property<int>("AgentId");

                    b.HasKey("CampaignId", "AgentId");

                    b.HasIndex("AgentId");

                    b.ToTable("CampaignAgents");
                });

            modelBuilder.Entity("Codes.Service.Models.CampaignModel", b =>
                {
                    b.Property<int>("CampaignId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrokerId");

                    b.Property<string>("CampaignDescription");

                    b.Property<string>("CampaignName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("CampaignType")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("CardQuantity");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CreatorIP")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("CustomCSS");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<DateTime?>("DeactivationReason");

                    b.Property<DateTime>("EndDateTime");

                    b.Property<string>("GoogleAnalyticsCode");

                    b.Property<bool>("IsActive");

                    b.Property<int>("PackageId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("StartDateTime");

                    b.HasKey("CampaignId");

                    b.HasIndex("BrokerId");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("Codes.Service.Models.ClientModel", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(255);

                    b.Property<int>("BrokerId");

                    b.Property<string>("City")
                        .HasMaxLength(100);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("ContactFirstName")
                        .HasMaxLength(255);

                    b.Property<string>("ContactLastName")
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CreatorIP")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<DateTime?>("DeactivationReason");

                    b.Property<string>("EIN")
                        .HasMaxLength(500);

                    b.Property<string>("Fax")
                        .HasMaxLength(50);

                    b.Property<string>("FaxExtension")
                        .HasMaxLength(50);

                    b.Property<bool>("IsActive");

                    b.Property<string>("MobilePhone")
                        .HasMaxLength(50);

                    b.Property<string>("OfficeExtension")
                        .HasMaxLength(50);

                    b.Property<string>("OfficePhone")
                        .HasMaxLength(50);

                    b.Property<string>("PostalCode")
                        .HasMaxLength(50);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("State")
                        .HasMaxLength(100);

                    b.HasKey("ClientId");

                    b.HasIndex("BrokerId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Codes.Service.Models.CodeActivityModel", b =>
                {
                    b.Property<int>("CodeActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActivationCode")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Address1")
                        .HasMaxLength(255);

                    b.Property<string>("Address2")
                        .HasMaxLength(255);

                    b.Property<string>("City")
                        .HasMaxLength(100);

                    b.Property<int>("CodeId");

                    b.Property<string>("CountryCode")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CreatorIP")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("EmailVerifiedDate");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Issuer")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(255);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Phone1")
                        .HasMaxLength(25);

                    b.Property<string>("Phone2")
                        .HasMaxLength(25);

                    b.Property<string>("PostalCode")
                        .HasMaxLength(50);

                    b.Property<int>("RSIId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("StateCode")
                        .HasMaxLength(50);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100);

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
                        .HasMaxLength(500);

                    b.Property<int>("CondoRewards");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CreatorIP")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<DateTime?>("EndDate");

                    b.Property<int>("HotelPoints");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Issuer")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("IssuerReference")
                        .HasMaxLength(100);

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

            modelBuilder.Entity("Codes.Service.Models.CodeRangeModel", b =>
                {
                    b.Property<int>("CodeRangeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrokerId");

                    b.Property<string>("CodeType")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<decimal>("CondoPoints");

                    b.Property<decimal>("Cost");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CreatorIP")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<DateTime?>("DeactivationReason");

                    b.Property<DateTime?>("EndDate");

                    b.Property<int>("EndNumber");

                    b.Property<decimal>("HotelPoints");

                    b.Property<int>("IncrementByNumber");

                    b.Property<bool>("IsActive");

                    b.Property<int>("NumberOfUses");

                    b.Property<int>("PackageId");

                    b.Property<string>("PostAlphaCharacters")
                        .HasMaxLength(50);

                    b.Property<string>("PreAlphaCharacters")
                        .HasMaxLength(50);

                    b.Property<int>("RSIOrganizationId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime?>("StartDate");

                    b.Property<int>("StartNumber");

                    b.Property<bool>("VerifyEmail");

                    b.HasKey("CodeRangeId");

                    b.HasIndex("BrokerId");

                    b.ToTable("CodeRanges");
                });

            modelBuilder.Entity("Codes.Service.Models.AgentModel", b =>
                {
                    b.HasOne("Codes.Service.Models.BrokerModel", "Broker")
                        .WithMany("Agents")
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Codes.Service.Models.CampaignAgentModel", b =>
                {
                    b.HasOne("Codes.Service.Models.AgentModel", "Agent")
                        .WithMany("CampaignAgents")
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Codes.Service.Models.CampaignModel", "Campaign")
                        .WithMany("CampaignAgents")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Codes.Service.Models.CampaignModel", b =>
                {
                    b.HasOne("Codes.Service.Models.BrokerModel", "Broker")
                        .WithMany()
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Codes.Service.Models.ClientModel", b =>
                {
                    b.HasOne("Codes.Service.Models.BrokerModel", "Broker")
                        .WithMany("Clients")
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Codes.Service.Models.CodeActivityModel", b =>
                {
                    b.HasOne("Codes.Service.Models.CodeModel", "Code")
                        .WithMany("CodeActivities")
                        .HasForeignKey("CodeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Codes.Service.Models.CodeRangeModel", b =>
                {
                    b.HasOne("Codes.Service.Models.BrokerModel", "Broker")
                        .WithMany()
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
