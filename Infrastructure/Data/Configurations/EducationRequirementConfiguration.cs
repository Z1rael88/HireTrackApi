using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class EducationRequirementConfiguration : BaseEntityConfiguration<EducationRequirement>
{
    public override void Configure(EntityTypeBuilder<EducationRequirement> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.EducationType).IsRequired();
        builder.Property(x => x.Degree).IsRequired();
    }
}