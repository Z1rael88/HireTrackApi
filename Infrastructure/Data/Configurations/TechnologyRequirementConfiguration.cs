using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class TechnologyRequirementConfiguration : BaseEntityConfiguration<TechnologyRequirement>
{
    public override void Configure(EntityTypeBuilder<TechnologyRequirement> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.YearsOfExperience).IsRequired();
        builder.HasOne(x=>x.TechnologyType)
            .WithMany()
            .HasForeignKey(x => x.TechnologyTypeId);
    }
}