using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollingSystem.Entities;

namespace PollingSystem.Infrastructure.Configurations
{
    public class SurveyConfigurations : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(s => s.Questions)
                .WithOne(q => q.Survey)
                .HasForeignKey(q => q.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.UserSurveys)
                .WithOne(u => u.Survey)
                .HasForeignKey(u => u.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
