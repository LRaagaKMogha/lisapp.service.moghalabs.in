using DEV.Common;
using Microsoft.EntityFrameworkCore;

namespace DEV.Model.EF.ReferrerWiseDue
{
    public partial class ReferrerWiseDueContext : DbContext
    {
        public string _connectionString = string.Empty;

        public ReferrerWiseDueContext (string connectionString)
        {
            _connectionString = connectionString;
        }

        public ReferrerWiseDueContext(DbContextOptions<ReferrerWiseDueContext> options) : base(options) {
        }

        public virtual DbSet<RefWiseDueResponseData> FetchRefWiseDueResponse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(EncryptionHelper.Decrypt(_connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Productversion", "2.2.3-servicing-35854");

            modelBuilder.Entity<RefWiseDueResponseData>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_LB_ReferrerWise_Due_Details");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
        }
    }
}
