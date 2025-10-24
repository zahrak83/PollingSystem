using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollingSystem.Entities;

namespace PollingSystem.Infrastructure.Configurations
{
    public class VoteConfigurations : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasOne(v => v.Survey)
                .WithMany(s => s.Votes)
                .HasForeignKey(v => v.SurveyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.Option)
                .WithMany(o => o.Votes)
                .HasForeignKey(v => v.OptionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.NormalUser)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.NormalUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
