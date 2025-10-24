using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollingSystem.Entities;

namespace PollingSystem.Infrastructure.Configurations
{
    public class NormalUserConfigurations : IEntityTypeConfiguration<NormalUser>
    {
        public void Configure(EntityTypeBuilder<NormalUser> builder)
        {
            builder.HasMany(u => u.Votes)
                .WithOne(v => v.NormalUser)
                .HasForeignKey(v => v.NormalUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(n => n.StatusSurveys)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
