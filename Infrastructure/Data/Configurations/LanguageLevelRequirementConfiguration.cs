using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class LanguageLevelRequirementConfiguration : BaseEntityConfiguration<LanguageLevelRequirement>
{
    public override void Configure(EntityTypeBuilder<LanguageLevelRequirement> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Level).IsRequired();
        builder.Property(x => x.Language).IsRequired();
    }
}