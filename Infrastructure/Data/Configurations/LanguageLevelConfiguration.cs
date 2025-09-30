using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class LanguageLevelConfiguration : BaseEntityConfiguration<LanguageLevel>
{
    public override void Configure(EntityTypeBuilder<LanguageLevel> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Level).IsRequired();
        builder.Property(x => x.Language).IsRequired();
    }
}