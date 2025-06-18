using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Codes1.Service.Models;

namespace Codes1.Service.Data
{
    public class Codes1DbContext : DbContext
    {
        public Codes1DbContext(DbContextOptions<Codes1DbContext> options)
            : base(options)
        { }

        public Codes1DbContext()
        {

        }

        public DbSet<BookingCommissionModel> BookingCommissions { get; set; }
        public DbSet<BookingCommissionBreakdownModel> BookingCommissionBreakdowns { get; set; }
        public DbSet<CodeModel> Codes { get; set; }
        public DbSet<CodeActivityModel> CodeActivities { get; set; }
        public DbSet<BulkCodeAuditModel> BulkCodeAudits { get; set; }
        public DbSet<BrokerModel> Brokers { get; set; }
        public DbSet<AdditionalCodeActivityModel> AdditionalCodeActivities { get; set; }
        public DbSet<CampaignModel> Campaigns { get; set; }
        public DbSet<AgentModel> Agents { get; set; }
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<CodeRangeModel> CodeRanges { get; set; }
        public DbSet<CampaignAgentModel> CampaignAgents { get; set; }
        public DbSet<CampaignCodeRangeModel> CampaignCodeRanges { get; set; }
        public DbSet<UsedCodeModel> UsedCodes { get; set; }
        public DbSet<UnusedCodeModel> UnusedCodes { get; set; }
        public DbSet<PendingCodeModel> PendingCodes { get; set; }
        public DbSet<VideoModel> Videos { get; set; }
        public DbSet<PurchaseModel> Purchases { get; set; }
        public DbSet<DocumentModel> Documents { get; set; }
        public DbSet<ClientAgentModel> ClientAgents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Remove the cascade delete default
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<CodeActivityModel>()
                .HasIndex(b => b.RSIId);

            modelBuilder.Entity<CodeModel>()
                .HasIndex(b => b.Issuer);

            modelBuilder.Entity<CampaignAgentModel>()
                .HasKey(t => new { t.CampaignId, t.AgentId });

            modelBuilder.Entity<CampaignAgentModel>()
                .HasOne(pt => pt.Campaign)
                .WithMany(p => p.CampaignAgents)
                .HasForeignKey(pt => pt.CampaignId);

            modelBuilder.Entity<CampaignAgentModel>()
                .HasOne(pt => pt.Agent)
                .WithMany(t => t.CampaignAgents)
                .HasForeignKey(pt => pt.AgentId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Password = @cc3ss124; Persist Security Info = True; User ID = RSGAccess; Initial Catalog = RSICodeGenerators; Data Source = 54.208.246.191; ");
        }
    }
}
