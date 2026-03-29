using Service.Common;
using Service.Model.External.CommonMasters;
using Microsoft.EntityFrameworkCore;

namespace Service.Model.EF.External.CommonMasters
{
    public class CustomerContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public CustomerContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }

        public virtual DbSet<LstCustomer> GetCustomer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(EncryptionHelper.DecryptSecret(_connectionstring));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<LstCustomer>(entity =>
            {
                entity.HasKey(e => e.customerNo);
                entity.ToTable("pro_Ex_GetCustomer");
                entity.Property(e => e.customerName).HasColumnName("customerName");
            });
        }
    }
}
