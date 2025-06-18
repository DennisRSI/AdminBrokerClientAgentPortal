using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using VendorImport.Service.Models;

namespace VendorImport.Service.Data
{
    public class VendorDbContext : DbContext
    {
        public VendorDbContext(DbContextOptions<VendorDbContext> options)
            : base(options)
        { }

        public VendorDbContext()
        {

        }

        public DbSet<AdjustmentModel> Adjustments { get; set; }
        public DbSet<InventoryReservationModel> InventoryReservations { get; set; }
        public DbSet<MerchantInventoryReservationModel> MerchantInventoryReservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdjustmentModel>()
                .HasIndex(b => b.Confirmation);

            modelBuilder.Entity<InventoryReservationModel>()
                .HasIndex(b => new { b.Confirmation, b.SiteId });
                

            modelBuilder.Entity<MerchantInventoryReservationModel>()
                .HasIndex(b => new { b.Confirmation, b.SiteId });

            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Password = @cc3ss124; Persist Security Info = True; User ID = RSGAccess; Initial Catalog = Vendors; Data Source = 54.208.246.191; ");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
