using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollingSystem.Entities;

namespace PollingSystem.Infrastructure.Configurations
{
    public class UserSurveyConfigurations : IEntityTypeConfiguration<UserSurvey>
    {
        public void Configure(EntityTypeBuilder<UserSurvey> builder)
        {
            builder.HasIndex(us => new { us.NormalUserId, us.SurveyId })
                .IsUnique();

            builder.HasOne(us => us.NormalUser)
                .WithMany(u => u.UserSurveys)
                .HasForeignKey(us => us.NormalUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(us => us.Survey)
                .WithMany(s => s.UserSurveys)
                .HasForeignKey(us => us.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
