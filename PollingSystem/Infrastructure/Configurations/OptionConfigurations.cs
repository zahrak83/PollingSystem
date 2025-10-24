using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollingSystem.Entities;

namespace PollingSystem.Infrastructure.Configurations
{
    public class OptionConfigurations : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.Property(o => o.Text)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasMany(o => o.Votes)
                .WithOne(v => v.Option)
                .HasForeignKey(v => v.OptionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
