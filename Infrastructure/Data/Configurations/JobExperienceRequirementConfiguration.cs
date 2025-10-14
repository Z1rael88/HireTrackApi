using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class JobExperienceRequirementConfiguration : BaseEntityConfiguration<JobExperienceRequirement>
{
    public override void Configure(EntityTypeBuilder<JobExperienceRequirement> builder)
    {
        base.Configure(builder);
        builder.HasMany(x => x.TechnologyRequirements)
            .WithOne(x=>x.JobExperienceRequirement)
            .HasForeignKey(x => x.JobExperienceRequirementId);
    }
}