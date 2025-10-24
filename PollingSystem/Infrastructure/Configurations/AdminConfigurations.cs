using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollingSystem.Entities;

namespace PollingSystem.Infrastructure.Configurations
{
    public class AdminConfigurations : IEntityTypeConfiguration<Admin>
    {
        void IEntityTypeConfiguration<Admin>.Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasMany(a => a.Surveys)
                .WithOne(s => s.Admin)
                .HasForeignKey(a => a.AdminId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
