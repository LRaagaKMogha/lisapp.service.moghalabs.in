using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QCManagement.Models;

namespace QCManagement.Entities
{
    public class ReagentEntityConfiguration : IEntityTypeConfiguration<Reagent>
    {
        public void Configure(EntityTypeBuilder<Reagent> builder)
        {
            builder.HasNoDiscriminator().HasKey(x => x.Identifier);
            builder.Property(x => x.LotSetupOrInstallationDate).IsRequired(false);
        }
    }
}
