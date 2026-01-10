using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterManagement.Entities;
using MasterManagement.Models;
using Microsoft.EntityFrameworkCore;
using DEV.Common;

namespace MasterManagement.Helpers
{
    public class MasterDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public MasterDataContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
            if (Products == null) Products = Set<Product>();
            if (ProductSpecialRequirements == null) ProductSpecialRequirements = Set<ProductSpecialRequirement>();
            if (Lookups == null) Lookups = Set<Lookup>();
            if (Nurses == null) Nurses = Set<Nurse>();
            if (Tariffs == null) Tariffs = Set<Tariff>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(EncryptionHelper.Decrypt(Configuration.GetConnectionString("WebApiDatabase")), options =>
            {

            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductSpecialRequirementEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LookupEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TariffEntityConfiguration());
            modelBuilder.ApplyConfiguration(new NurseEntityConfiguration());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSpecialRequirement> ProductSpecialRequirements { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }

    }
}